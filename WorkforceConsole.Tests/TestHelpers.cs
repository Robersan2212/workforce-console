using Microsoft.EntityFrameworkCore;
using WorkforceConsole;

namespace WorkforceConsole.Tests;

public static class TestHelpers
{
    public static WorkforceContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<WorkforceContext>()
            .UseSqlite("DataSource=:memory:")
            .Options;

        var context = new WorkforceContext(options);
        context.Database.OpenConnection();
        context.Database.EnsureCreated();
        return context;
    }
}
