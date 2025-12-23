using EmployeeExitPortal.Api.Data;
using EmployeeExitPortal.Api.DTO;
using EmployeeExitPortal.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;

namespace EmployeeExitPortal.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly JwtService _jwtService;

        public AuthController(AppDbContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Tz == request.Tz && u.IsActive);

            if (user == null)
                return Unauthorized("משתמש לא קיים");

            // בדיקת סיסמה (BCrypt)
            bool passwordOk = BCrypt.Net.BCrypt.Verify(
                request.Password,
                user.PasswordHash
            );

            if (!passwordOk)
                return Unauthorized("סיסמה שגויה");

            var token = _jwtService.CreateToken(user);

            return Ok(new
            {
                token,
                displayName = user.DisplayName,
                role = user.Role
            });
        }
    }
}
