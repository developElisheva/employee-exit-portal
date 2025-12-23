using EmployeeExitPortal.Api.Data;
using EmployeeExitPortal.Api.DTO;
using Microsoft.EntityFrameworkCore;

namespace EmployeeExitPortal.Api.Services
{
    public class ExitFormService
    {
        private readonly AppDbContext _context;

        public ExitFormService(AppDbContext context)
        {
            _context = context;
        }

        // =========================
        // HR – רשימת טפסים
        // =========================
        public async Task<List<ExitFormSummaryDto>> GetForHrAsync()
        {
            return await _context.ExitForms
                .Include(f => f.Employee)
                .Include(f => f.Tasks)
                .Select(f => new ExitFormSummaryDto
                {
                    Id = f.Id,
                    EmployeeName = f.Employee.FullName,
                    EmployeeTz = f.Employee.Tz,
                    EndDate = f.ExitDate,
                    IsCompleted =
                        f.Tasks.Any() &&
                        f.Tasks.All(t => t.Status == "Approved")
                })
                .ToListAsync();
        }

        // =========================
        // טופס בודד לפי הרשאות
        // =========================
        public async Task<ExitFormDetailsDto?> GetDetailsAsync(
            int formId,
            string role,
            string? department)
        {
            var form = await _context.ExitForms
                .Include(f => f.Employee)
                .Include(f => f.Tasks)
                    .ThenInclude(t => t.ApprovedByUser)
                .FirstOrDefaultAsync(f => f.Id == formId);

            if (form == null)
                return null;

            var tasks = form.Tasks.AsQueryable();

            // Signer רואה רק את התחום שלו
            if (role == "Signer" && department != null)
            {
                tasks = tasks.Where(t => t.ResponsibleRole == department);
            }

            return new ExitFormDetailsDto
            {
                Id = form.Id,
                EmployeeName = form.Employee.FullName,
                EmployeeTz = form.Employee.Tz,
                EndDate = form.ExitDate,
                Tasks = tasks.Select(t => new ExitTaskDto
                {
                    Id = t.Id,
                    Department = t.ResponsibleRole,
                    Topic = t.Title,
                    IsApproved = t.Status == "Approved",
                    ApprovedBy = t.ApprovedByUser == null
                    ? null
                    : t.ApprovedByUser.DisplayName,
                    ApprovedAt = t.ApprovedAt,
                    Comment = t.Comments
                }).ToList()
            };
        }

        // =========================
        // ⭐ טפסים עם המשימות של המחלקה שלי
        // =========================
        public async Task<List<ExitFormDetailsDto>> GetForDepartmentAsync(string department)
        {
            var forms = await _context.ExitForms
                .Include(f => f.Employee)
                .Include(f => f.Tasks)
                    .ThenInclude(t => t.ApprovedByUser)
                .Where(f => f.Tasks.Any(t => t.ResponsibleRole == department))
                .ToListAsync();

            return forms.Select(f => new ExitFormDetailsDto
            {
                Id = f.Id,
                EmployeeName = f.Employee.FullName,
                EmployeeTz = f.Employee.Tz,
                EndDate = f.ExitDate,
                Tasks = f.Tasks
                    .Where(t => t.ResponsibleRole == department)
                    .Select(t => new ExitTaskDto
                    {
                        Id = t.Id,
                        Department = t.ResponsibleRole,
                        Topic = t.Title,
                        IsApproved = t.Status == "Approved",
                        ApprovedBy = t.ApprovedByUser?.DisplayName,
                        ApprovedAt = t.ApprovedAt,
                        Comment = t.Comments
                    })
                    .ToList()
            }).ToList();
        }
    }
}
