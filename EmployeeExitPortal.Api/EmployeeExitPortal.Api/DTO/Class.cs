using System;
using System.Collections.Generic;

namespace EmployeeExitPortal.Api.DTOs
{
    public class TasksByEmployeeDto
    {
        public int ExitFormId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime ExitDate { get; set; }
        public List<MyTaskDto> Tasks { get; set; }
    }
}
