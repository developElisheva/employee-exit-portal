using System;
using System.Collections.Generic;

namespace EmployeeExitPortal.Api.Models
{
    public class ExitForm
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public DateTime ExitDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Open";

        public ICollection<ExitTask> Tasks { get; set; }
    }
}
