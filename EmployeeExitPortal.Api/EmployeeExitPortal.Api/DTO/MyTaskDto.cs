namespace EmployeeExitPortal.Api.DTOs
{
    public class MyTaskDto
    {
        public int TaskId { get; set; }
        public int ExitFormId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime ExitDate { get; set; }

        public string Title { get; set; }
        public string ResponsibleRole { get; set; }
        public string Status { get; set; }
        public string? Comments { get; set; }
        public DateTime? ApprovedAt { get; set; }
    }
}
