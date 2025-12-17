using EmployeeExitPortal.Api.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;

namespace EmployeeExitPortal.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<ExitForm> ExitForms { get; set; }
        public DbSet<ExitTask> ExitTasks { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
