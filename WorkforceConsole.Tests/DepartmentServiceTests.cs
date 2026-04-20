using WorkforceConsole;

namespace WorkforceConsole.Tests;

public class DepartmentServiceTests : IDisposable
{
    private readonly WorkforceContext _context;
    private readonly DepartmentService _service;

    public DepartmentServiceTests()
    {
        _context = TestHelpers.CreateContext();
        _service = new DepartmentService(_context);
    }

    public void Dispose() => _context.Dispose();

    [Fact]
    public void AddDepartment_SavesAndIsRetrievable()
    {
        _service.AddDepartment(new Department { Name = "HR" });

        Assert.Single(_context.Departments);
        Assert.Equal("HR", _context.Departments.First().Name);
    }

    [Fact]
    public void GetAllDepartments_WhenNoneExist_ReturnsEmptyList()
    {
        var result = _service.GetAllDepartments();

        Assert.Empty(result);
    }

    [Fact]
    public void GetAllDepartments_WhenMultipleExist_ReturnsAll()
    {
        _service.AddDepartment(new Department { Name = "HR" });
        _service.AddDepartment(new Department { Name = "Engineering" });

        var result = _service.GetAllDepartments();

        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void GetDepartmentById_WhenFound_ReturnsDepartment()
    {
        _service.AddDepartment(new Department { Name = "Finance" });
        var id = _context.Departments.First().DepartmentId;

        var result = _service.GetDepartmentById(id);

        Assert.NotNull(result);
        Assert.Equal("Finance", result.Name);
    }

    [Fact]
    public void GetDepartmentById_WhenNotFound_ReturnsNull()
    {
        var result = _service.GetDepartmentById(999);

        Assert.Null(result);
    }

    [Fact]
    public void UpdateDepartment_PersistsNameChange()
    {
        _service.AddDepartment(new Department { Name = "OldName" });
        var dept = _context.Departments.First();
        dept.Name = "NewName";

        _service.UpdateDepartment(dept);

        Assert.Equal("NewName", _context.Departments.First().Name);
    }

    [Fact]
    public void DeleteDepartment_RemovesDepartment()
    {
        _service.AddDepartment(new Department { Name = "ToDelete" });
        var id = _context.Departments.First().DepartmentId;

        _service.DeleteDepartment(id);

        Assert.Empty(_context.Departments);
    }

    [Fact]
    public void DeleteDepartment_WhenNotFound_DoesNotThrow()
    {
        var exception = Record.Exception(() => _service.DeleteDepartment(999));

        Assert.Null(exception);
    }
}
