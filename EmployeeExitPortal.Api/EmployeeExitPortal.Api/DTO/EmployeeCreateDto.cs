namespace EmployeeExitPortal.Api.DTO
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Tz { get; set; } = null!;
        public string Unit { get; set; } = null!;
    }
}
