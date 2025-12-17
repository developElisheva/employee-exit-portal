using EmployeeExitPortal.Api.Data;
using EmployeeExitPortal.Api.Models;
using EmployeeExitPortal.Api.DTOs;
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

        // יצירת טופס טיולים חדש לעובד
        public async Task<ExitForm?> CreateExitFormAsync(int employeeId, DateTime exitDate)
        {
            var employee = await _context.Employees.FindAsync(employeeId);
            if (employee == null)
                return null;

            var form = new ExitForm
            {
                EmployeeId = employeeId,
                ExitDate = exitDate,
                Status = "Open",
                CreatedAt = DateTime.UtcNow
            };

            _context.ExitForms.Add(form);
            await _context.SaveChangesAsync();

            return form;
        }

        // שליפת טופס DTO להצגה
        public async Task<ExitFormDto?> GetFormDtoAsync(int id)
        {
            var form = await _context.ExitForms
                .Include(f => f.Employee)
                .Include(f => f.Tasks)
                .ThenInclude(t => t.ApprovedByUser)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (form == null)
                return null;

            return new ExitFormDto
            {
                Id = form.Id,
                EmployeeName = form.Employee.FullName,
                Unit = form.Employee.Unit,
                ExitDate = form.ExitDate,
                Status = form.Status,
                Tasks = form.Tasks.Select(t => new ExitTaskDto
                {
                    Title = t.Title,
                    ResponsibleRole = t.ResponsibleRole,
                    Status = t.Status,
                    Comments = t.Comments
                }).ToList()
            };
        }

        // שליפת טפסים לפי עובד מסוים
        public async Task<List<ExitFormDto>> GetFormsForEmployeeAsync(int employeeId)
        {
            var forms = await _context.ExitForms
                .Include(f => f.Employee)
                .Include(f => f.Tasks)
                .Where(f => f.EmployeeId == employeeId)
                .ToListAsync();

            return forms.Select(f => new ExitFormDto
            {
                Id = f.Id,
                EmployeeName = f.Employee.FullName,
                Unit = f.Employee.Unit,
                ExitDate = f.ExitDate,
                Status = f.Status,
                Tasks = f.Tasks.Select(t => new ExitTaskDto
                {
                    Title = t.Title,
                    ResponsibleRole = t.ResponsibleRole,
                    Status = t.Status,
                    Comments = t.Comments
                }).ToList()
            }).ToList();
        }
    }
}
