namespace EmployeeExitPortal.Api.DTO
{
    public class ExitTaskDto
    {
        public int Id { get; set; }
        public string Department { get; set; }
        public string Topic { get; set; }
        public bool IsApproved { get; set; }
        public string? ApprovedBy { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public string? Comment { get; set; }
    }
}
