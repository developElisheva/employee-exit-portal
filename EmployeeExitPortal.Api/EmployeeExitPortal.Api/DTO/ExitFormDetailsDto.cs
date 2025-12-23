namespace EmployeeExitPortal.Api.DTO
{
    public class ExitFormDetailsDto
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeTz { get; set; }
        public DateTime EndDate { get; set; }
        public List<ExitTaskDto> Tasks { get; set; }
    }
}
