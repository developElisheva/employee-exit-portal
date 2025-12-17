using Microsoft.AspNetCore.Components.Forms;

namespace EmployeeExitPortal.Api.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Tz { get; set; }
        public string Unit { get; set; }

        public ICollection<ExitForm> ExitForms { get; set; }
    }
}
