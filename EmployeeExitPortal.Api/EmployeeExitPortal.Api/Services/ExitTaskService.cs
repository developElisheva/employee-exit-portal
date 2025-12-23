using EmployeeExitPortal.Api.Data;
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

        public async Task CreateTasksForExitFormAsync(int formId)
        {
            var tasks = new[]
            {
                ("IT","מחשב נייד"),
                ("Security","כרטיס חכם"),
                ("IT","סגירת הרשאות"),
                ("Operations","מפתחות"),
                ("Parking","חניה"),
            };

            foreach (var (dept, title) in tasks)
            {
                _context.ExitTasks.Add(new Models.ExitTask
                {
                    ExitFormId = formId,
                    ResponsibleRole = dept,
                    Title = title,
                    Status = "Pending"
                });
            }

            await _context.SaveChangesAsync();
        }

        public async Task ApproveAsync(
            int taskId, int userId, string? comment)
        {
            var task = await _context.ExitTasks
                .Include(t => t.ExitForm)
                    .ThenInclude(f => f.Tasks)
                .FirstOrDefaultAsync(t => t.Id == taskId);

            if (task == null) return;

            task.Status = "Approved";
            task.ApprovedAt = DateTime.UtcNow;
            task.ApprovedByUserId = userId;
            task.Comments = comment;

            if (task.ExitForm.Tasks.All(t => t.Status == "Approved"))
                task.ExitForm.Status = "Completed";

            await _context.SaveChangesAsync();
        }
    }
}
