using EmployeeExitPortal.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EmployeeExitPortal.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ExitFormsController : ControllerBase
    {
        private readonly ExitFormService _service;

        public ExitFormsController(ExitFormService service)
        {
            _service = service;
        }

        // =========================
        // HR – רשימת טפסים
        // =========================
        [Authorize(Roles = "HR")]
        [HttpGet("hr")]
        public async Task<IActionResult> HrList()
            => Ok(await _service.GetForHrAsync());

        // =========================
        // טופס בודד לפי הרשאות
        // =========================
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var role = User.FindFirstValue(ClaimTypes.Role);
            var department = User.FindFirstValue("department");

            var result = await _service.GetDetailsAsync(id, role!, department);
            return result == null ? NotFound() : Ok(result);
        }

        // =========================
        // ⭐ טפסים עם המשימות שלי (לפי Department)
        // =========================
        [HttpGet("for-role")]
        public async Task<IActionResult> GetForMyDepartment()
        {
            var department = User.FindFirstValue("department");

            if (string.IsNullOrWhiteSpace(department))
                return BadRequest("department claim is missing");

            var result = await _service.GetForDepartmentAsync(department);
            return Ok(result);
        }
    }
}
