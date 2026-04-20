using Microsoft.EntityFrameworkCore;
using System.IO;

namespace WorkforceConsole
{
    public class WorkforceContext : DbContext
    {
        public WorkforceContext() { }

        public WorkforceContext(DbContextOptions<WorkforceContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "workforce.db");
                optionsBuilder.UseSqlite($"Data Source={dbPath}");
            }
        }
    }
} 