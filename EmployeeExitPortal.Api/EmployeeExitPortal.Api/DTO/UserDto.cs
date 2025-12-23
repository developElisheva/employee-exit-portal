public class UserDto
{
    public int Id { get; set; }
    public string DisplayName { get; set; }
    public string Tz { get; set; }
    public string OrgUserName { get; set; }
    public string Role { get; set; }
    public string? Department { get; set; }   
    public bool IsActive { get; set; }
    public string? Password { get; set; }
}
