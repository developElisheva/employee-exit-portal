using System;

namespace EmployeeExitPortal.Api.Models
{
    public class ExitTask
    {
        public int Id { get; set; }

        public int ExitFormId { get; set; }
        public ExitForm ExitForm { get; set; }

        public string Title { get; set; }
        public string ResponsibleRole { get; set; }

        public string Status { get; set; } = "Pending";

        public string? Comments { get; set; } 

        public int? ApprovedByUserId { get; set; }
        public User? ApprovedByUser { get; set; }

        public DateTime? ApprovedAt { get; set; }
    }
}
