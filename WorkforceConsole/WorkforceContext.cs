using Microsoft.EntityFrameworkCore;
using System.IO;

namespace WorkforceConsole
{
    public class WorkforceContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Use absolute path to ensure database is found
            var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "workforce.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }
    }
} 