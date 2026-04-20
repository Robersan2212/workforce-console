using WorkforceConsole;

namespace WorkforceConsole.Tests;

public class EmployeeServiceTests : IDisposable
{
    private readonly WorkforceContext _context;
    private readonly EmployeeService _service;
    private readonly DepartmentService _departmentService;

    public EmployeeServiceTests()
    {
        _context = TestHelpers.CreateContext();
        _service = new EmployeeService(_context);
        _departmentService = new DepartmentService(_context);
    }

    public void Dispose() => _context.Dispose();

    [Fact]
    public void AddEmployee_WithoutDepartment_SavesEmployee()
    {
        _service.AddEmployee(new Employee { FirstName = "Jane", LastName = "Doe" });

        Assert.Single(_context.Employees);
    }

    [Fact]
    public void AddEmployee_WithValidDepartment_SavesEmployee()
    {
        _departmentService.AddDepartment(new Department { Name = "Engineering" });
        var dept = _context.Departments.First();

        _service.AddEmployee(new Employee { FirstName = "John", LastName = "Smith", DepartmentId = dept.DepartmentId });

        Assert.Single(_context.Employees);
        Assert.Equal(dept.DepartmentId, _context.Employees.First().DepartmentId);
    }

    [Fact]
    public void AddEmployee_WithNonExistentDepartment_DoesNotSave()
    {
        _service.AddEmployee(new Employee { FirstName = "Ghost", LastName = "User", DepartmentId = 999 });

        Assert.Empty(_context.Employees);
    }

    [Fact]
    public void GetAllEmployees_WhenNoneExist_ReturnsEmptyList()
    {
        var result = _service.GetAllEmployees();

        Assert.Empty(result);
    }

    [Fact]
    public void GetAllEmployees_WhenMultipleExist_ReturnsAll()
    {
        _service.AddEmployee(new Employee { FirstName = "Alice", LastName = "A" });
        _service.AddEmployee(new Employee { FirstName = "Bob", LastName = "B" });

        var result = _service.GetAllEmployees();

        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void GetEmployeeById_WhenFound_ReturnsEmployee()
    {
        _service.AddEmployee(new Employee { FirstName = "Alice", LastName = "A" });
        var id = _context.Employees.First().EmployeeId;

        var result = _service.GetEmployeeById(id);

        Assert.NotNull(result);
        Assert.Equal("Alice", result.FirstName);
    }

    [Fact]
    public void GetEmployeeById_WhenNotFound_ReturnsNull()
    {
        var result = _service.GetEmployeeById(999);

        Assert.Null(result);
    }

    [Fact]
    public void UpdateEmployee_PersistsChanges()
    {
        _service.AddEmployee(new Employee { FirstName = "Old", LastName = "Name" });
        var emp = _context.Employees.First();
        emp.FirstName = "New";

        _service.UpdateEmployee(emp);

        Assert.Equal("New", _context.Employees.First().FirstName);
    }

    [Fact]
    public void DeleteEmployee_RemovesEmployee()
    {
        _service.AddEmployee(new Employee { FirstName = "ToDelete", LastName = "User" });
        var id = _context.Employees.First().EmployeeId;

        _service.DeleteEmployee(id);

        Assert.Empty(_context.Employees);
    }

    [Fact]
    public void DeleteEmployee_WhenNotFound_DoesNotThrow()
    {
        var exception = Record.Exception(() => _service.DeleteEmployee(999));

        Assert.Null(exception);
    }
}
