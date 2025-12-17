using Microsoft.AspNetCore.Mvc;
using EmployeeExitPortal.Api.Services;
using EmployeeExitPortal.Api.DTOs;

namespace EmployeeExitPortal.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExitFormsController : ControllerBase
    {
        private readonly ExitFormService _exitFormService;
        private readonly ExitTaskService _taskService;

        public ExitFormsController(ExitFormService exitFormService, ExitTaskService taskService)
        {
            _exitFormService = exitFormService;
            _taskService = taskService;
        }

        // 1️⃣ יצירת טופס חדש לעובד
        [HttpPost]
        public async Task<IActionResult> CreateForm([FromBody] ExitFormCreateDto dto)
        {
            var form = await _exitFormService.CreateExitFormAsync(dto.EmployeeId, dto.ExitDate);
            if (form == null)
                return NotFound("Employee not found");

            // יצירת משימות אוטומטיות לטופס
            await _taskService.CreateTasksForExitFormAsync(form.Id);

            // החזרה כ-DTO
            var result = await _exitFormService.GetFormDtoAsync(form.Id);

            return Ok(result);
        }

        // 2️⃣ שליפת טופס לפי ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetForm(int id)
        {
            var form = await _exitFormService.GetFormDtoAsync(id);
            if (form == null)
                return NotFound();

            return Ok(form);
        }
    }
}
