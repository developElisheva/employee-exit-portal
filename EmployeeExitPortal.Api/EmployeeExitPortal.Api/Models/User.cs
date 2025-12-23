namespace EmployeeExitPortal.Api.Models
{
    public class User
    {
        public int Id { get; set; }

        public string DisplayName { get; set; } = null!;
        public string Tz { get; set; } = null!;
        public string OrgUserName { get; set; } = null!;
        public string Role { get; set; } = "Viewer";
        public string? Department { get; set; }
        public string PasswordHash { get; set; } = null!;
        public bool IsActive { get; set; } = true;
    }
}
