using EmployeeExitPortal.Api.Data;
using EmployeeExitPortal.Api.DTO;
using EmployeeExitPortal.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeExitPortal.Api.Services
{
    public class EmployeeService
    {
        private readonly AppDbContext _context;

        public EmployeeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmployeeDto>> GetAllAsync()
        {
            return await _context.Employees
                .Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    FullName = e.FullName,
                    Tz = e.Tz,
                    Unit = e.Unit
                })
                .ToListAsync();
        }

        public async Task<EmployeeDto?> GetByIdAsync(int id)
        {
            var e = await _context.Employees.FindAsync(id);
            if (e == null) return null;

            return new EmployeeDto
            {
                Id = e.Id,
                FullName = e.FullName,
                Tz = e.Tz,
                Unit = e.Unit
            };
        }

        public async Task<EmployeeDto> CreateAsync(EmployeeDto dto)
        {
            var e = new Employee
            {
                FullName = dto.FullName,
                Tz = dto.Tz,
                Unit = dto.Unit
            };

            _context.Employees.Add(e);
            await _context.SaveChangesAsync();

            dto.Id = e.Id;
            return dto;
        }

        public async Task UpdateAsync(int id, EmployeeDto dto)
        {
            var e = await _context.Employees.FindAsync(id);
            if (e == null) return;

            e.FullName = dto.FullName;
            e.Tz = dto.Tz;
            e.Unit = dto.Unit;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var e = await _context.Employees.FindAsync(id);
            if (e == null) return;

            _context.Employees.Remove(e);
            await _context.SaveChangesAsync();
        }
    }
}
