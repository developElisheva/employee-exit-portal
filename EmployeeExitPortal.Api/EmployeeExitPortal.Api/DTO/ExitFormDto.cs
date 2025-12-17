using System.Collections.Generic;

namespace EmployeeExitPortal.Api.DTOs
{
    public class ExitFormDto
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string Unit { get; set; }
        public DateTime ExitDate { get; set; }
        public string Status { get; set; }
        public List<ExitTaskDto> Tasks { get; set; }
    }
}
