using EmployeeExitPortal.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EmployeeExitPortal.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ExitTasksController : ControllerBase
    {
        private readonly ExitTaskService _service;

        public ExitTasksController(ExitTaskService service)
        {
            _service = service;
        }

        [HttpPost("{taskId}/approve")]
        public async Task<IActionResult> Approve(
            int taskId,
            [FromBody] string? comment)
        {
            var userId = int.Parse(User.FindFirstValue("userId")!);

            await _service.ApproveAsync(taskId, userId, comment);
            return Ok();
        }
    }
}
