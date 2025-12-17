using Microsoft.AspNetCore.Mvc;
using EmployeeExitPortal.Api.Services;

namespace EmployeeExitPortal.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExitTasksController : ControllerBase
    {
        private readonly ExitTaskService _taskService;

        public ExitTasksController(ExitTaskService taskService)
        {
            _taskService = taskService;
        }

        // 🔹 שליפת משימות מקובצות לפי Role
        [HttpGet("grouped")]
        public async Task<IActionResult> GetGroupedTasks([FromQuery] string role)
        {
            var tasks = await _taskService.GetGroupedTasksAsync(role);
            return Ok(tasks);
        }

        // 🔹 שליפת כל התחומים (Roles) – דינמי מה־DB
        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _taskService.GetAvailableRolesAsync();
            return Ok(roles);
        }

        // 🔹 אישור משימה (חתימה)
        [HttpPost("{taskId}/approve")]
        public async Task<IActionResult> ApproveTask(
            int taskId,
            [FromQuery] string role,
            [FromBody] string? comments)
        {
            var success = await _taskService.ApproveTaskAsync(taskId, role, comments);

            if (!success)
                return BadRequest("Cannot approve task (invalid role or task not found)");

            return Ok("Task approved successfully");
        }
    }
}
