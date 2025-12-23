using EmployeeExitPortal.Api.Data;
using EmployeeExitPortal.Api.DTO;
using EmployeeExitPortal.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EmployeeExitPortal.Api.Services
{
    public class UsersService
    {
        private readonly AppDbContext _context;

        public UsersService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserDto>> GetAllAsync()
        {
            return await _context.Users
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    DisplayName = u.DisplayName,
                    Tz = u.Tz,
                    OrgUserName = u.OrgUserName,
                    Role = u.Role,
                    Department = u.Department,
                    IsActive = u.IsActive
                })
                .ToListAsync();
        }

        public async Task<UserDto?> GetByIdAsync(int id)
        {
            var u = await _context.Users.FindAsync(id);
            if (u == null) return null;

            return new UserDto
            {
                Id = u.Id,
                DisplayName = u.DisplayName,
                Tz = u.Tz,
                OrgUserName = u.OrgUserName,
                Role = u.Role,
                Department = u.Department,
                IsActive = u.IsActive
            };
        }

        public async Task<UserDto> CreateAsync(UserDto dto)
        {
            var user = new User
            {
                DisplayName = dto.DisplayName,
                Tz = dto.Tz,
                OrgUserName = dto.OrgUserName,
                Role = dto.Role,
                Department = dto.Department,
                IsActive = dto.IsActive,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            dto.Id = user.Id;
            dto.Password = null;
            return dto;
        }

        public async Task UpdateAsync(int id, UserDto dto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) throw new Exception("User not found");

            user.DisplayName = dto.DisplayName;
            user.Role = dto.Role;
            user.Department = dto.Department;
            user.IsActive = dto.IsActive;

            if (!string.IsNullOrEmpty(dto.Password))
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetCurrentUserAsync(ClaimsPrincipal principal)
        {
            var id = principal.FindFirstValue("userId");
            return int.TryParse(id, out var userId)
                ? await _context.Users.FindAsync(userId)
                : null;
        }
    }
}
