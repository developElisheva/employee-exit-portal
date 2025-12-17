using EmployeeExitPortal.Api.Data;
using EmployeeExitPortal.Api.DTOs;
using EmployeeExitPortal.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeExitPortal.Api.Services
{
    public class ExitTaskService
    {
        private readonly AppDbContext _context;

        public ExitTaskService(AppDbContext context)
        {
            _context = context;
        }

        // 1️⃣ יצירת משימות אוטומטיות
        public async Task CreateTasksForExitFormAsync(int formId)
        {
            var defaultTasks = new List<ExitTask>
            {
                new ExitTask { Title = "מחשב נייד", ResponsibleRole = "IT", ExitFormId = formId },
                new ExitTask { Title = "כרטיס חכם", ResponsibleRole = "Security", ExitFormId = formId },
                new ExitTask { Title = "סגירת הרשאות", ResponsibleRole = "IT", ExitFormId = formId },
                new ExitTask { Title = "מפתחות משרד", ResponsibleRole = "Operations", ExitFormId = formId },
                new ExitTask { Title = "חניה / פנגו", ResponsibleRole = "Parking", ExitFormId = formId },
                new ExitTask { Title = "סלולרי", ResponsibleRole = "IT", ExitFormId = formId }
            };

            _context.ExitTasks.AddRange(defaultTasks);
            await _context.SaveChangesAsync();
        }

        // 2️⃣ משימות מקובצות לפי עובד
        public async Task<List<TasksByEmployeeDto>> GetGroupedTasksAsync(string role)
        {
            var tasks = await _context.ExitTasks
                .Include(t => t.ExitForm)
                .ThenInclude(f => f.Employee)
                .Where(t => t.ResponsibleRole == role)
                .ToListAsync();

            return tasks
                .GroupBy(t => new {
                    t.ExitFormId,
                    t.ExitForm.Employee.FullName,
                    t.ExitForm.ExitDate
                })
                .Select(group => new TasksByEmployeeDto
                {
                    ExitFormId = group.Key.ExitFormId,
                    EmployeeName = group.Key.FullName,
                    ExitDate = group.Key.ExitDate,
                    Tasks = group.Select(t => new MyTaskDto
                    {
                        TaskId = t.Id,
                        ExitFormId = t.ExitFormId,
                        EmployeeName = t.ExitForm.Employee.FullName,
                        ExitDate = t.ExitForm.ExitDate,
                        Title = t.Title,
                        ResponsibleRole = t.ResponsibleRole,
                        Status = t.Status,
                        Comments = t.Comments,
                        ApprovedAt = t.ApprovedAt
                    }).ToList()
                })
                .ToList();
        }

        // 3️⃣ אישור משימה + בדיקת השלמת טופס
        public async Task<bool> ApproveTaskAsync(int taskId, string role, string? comments = null)
        {
            var task = await _context.ExitTasks
                .Include(t => t.ExitForm)
                .ThenInclude(f => f.Tasks)
                .FirstOrDefaultAsync(t => t.Id == taskId);

            if (task == null)
                return false;

            // הרשאת אחראי
            if (task.ResponsibleRole != role)
                return false;

            // עדכון המשימה
            task.Status = "Approved";
            task.Comments = comments;
            task.ApprovedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            // 4️⃣ בדיקה: האם כל המשימות בטופס אושרו?
            bool allApproved = task.ExitForm.Tasks.All(t => t.Status == "Approved");

            if (allApproved)
            {
                task.ExitForm.Status = "Completed";
                await _context.SaveChangesAsync();
            }

            return true;
        }

        // 5️⃣ שליפת כל התחומים (Roles) – דינמי מתוך ExitTasks
        public async Task<List<string>> GetAvailableRolesAsync()
        {
            return await _context.ExitTasks
                .Select(t => t.ResponsibleRole)
                .Distinct()
                .OrderBy(r => r)
                .ToListAsync();
        }

    }
}
