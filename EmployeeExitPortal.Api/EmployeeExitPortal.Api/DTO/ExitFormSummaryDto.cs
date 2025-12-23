namespace EmployeeExitPortal.Api.DTO
{
    public class ExitFormSummaryDto
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeTz { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}
