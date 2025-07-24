using System;
using System.Linq;

namespace WorkforceConsole
{
    public class WorkforceApp
    {
        public void Run()
        {
            using (var context = new WorkforceContext())
            {
                // Ensure database is created and initialized
                context.Database.EnsureCreated();
                
                var employeeService = new EmployeeService(context);
                var departmentService = new DepartmentService(context);
                bool exit = false;
                while (!exit)
                {
                    Console.Clear();
                    Console.WriteLine("=== Workforce Console ===");
                    Console.WriteLine("1. Manage Employees");
                    Console.WriteLine("2. Manage Departments");
                    Console.WriteLine("0. Exit");
                    Console.Write("Select an option: ");
                    var input = Console.ReadLine();
                    switch (input)
                    {
                        case "1":
                            EmployeeMenu(employeeService, departmentService);
                            break;
                        case "2":
                            DepartmentMenu(departmentService);
                            break;
                        case "0":
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid option. Press any key to try again.");
                            Console.ReadKey();
                            break;
                    }
                }
            }
        }

        private void EmployeeMenu(EmployeeService employeeService, DepartmentService departmentService)
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.WriteLine("--- Employee Management ---");
                Console.WriteLine("1. List Employees");
                Console.WriteLine("2. Add Employee");
                Console.WriteLine("3. Update Employee");
                Console.WriteLine("4. Delete Employee");
                Console.WriteLine("0. Back");
                Console.Write("Select an option: ");
                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        var employees = employeeService.GetAllEmployees();
                        Console.WriteLine("\nEmployees:");
                        foreach (var e in employees)
                        {
                            Console.WriteLine($"ID: {e.EmployeeId}, Name: {e.FirstName} {e.LastName}, Dept: {e.DepartmentId}");
                        }
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                    case "2":
                        Console.Write("First Name: ");
                        var firstName = Console.ReadLine();
                        Console.Write("Last Name: ");
                        var lastName = Console.ReadLine();
                        
                        // Show available departments
                        var departments = departmentService.GetAllDepartments();
                        if (departments.Any())
                        {
                            Console.WriteLine("\nAvailable Departments:");
                            foreach (var d in departments)
                            {
                                Console.WriteLine($"ID: {d.DepartmentId}, Name: {d.Name}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nNo departments available. Please add departments first.");
                        }
                        
                        Console.Write("Department ID (optional, press Enter to skip): ");
                        var deptInput = Console.ReadLine();
                        int? deptId = null;
                        if (!string.IsNullOrWhiteSpace(deptInput) && int.TryParse(deptInput, out int parsedDeptId))
                            deptId = parsedDeptId;
                        
                        var newEmp = new Employee { FirstName = firstName, LastName = lastName, DepartmentId = deptId };
                        employeeService.AddEmployee(newEmp);
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                    case "3":
                        Console.Write("Employee ID to update: ");
                        if (int.TryParse(Console.ReadLine(), out int updateId))
                        {
                            var emp = employeeService.GetEmployeeById(updateId);
                            if (emp != null)
                            {
                                Console.Write($"First Name ({emp.FirstName}): ");
                                var fn = Console.ReadLine();
                                if (!string.IsNullOrWhiteSpace(fn)) emp.FirstName = fn;
                                Console.Write($"Last Name ({emp.LastName}): ");
                                var ln = Console.ReadLine();
                                if (!string.IsNullOrWhiteSpace(ln)) emp.LastName = ln;
                                Console.Write($"Department ID ({emp.DepartmentId}): ");
                                var dn = Console.ReadLine();
                                if (int.TryParse(dn, out int newDeptId)) emp.DepartmentId = newDeptId;
                                employeeService.UpdateEmployee(emp);
                                Console.WriteLine("Employee updated. Press any key to continue...");
                            }
                            else
                            {
                                Console.WriteLine("Employee not found.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid ID.");
                        }
                        Console.ReadKey();
                        break;
                    case "4":
                        Console.Write("Employee ID to delete: ");
                        if (int.TryParse(Console.ReadLine(), out int delId))
                        {
                            employeeService.DeleteEmployee(delId);
                            Console.WriteLine("Employee deleted (if existed). Press any key to continue...");
                        }
                        else
                        {
                            Console.WriteLine("Invalid ID.");
                        }
                        Console.ReadKey();
                        break;
                    case "0":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Press any key to try again.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void DepartmentMenu(DepartmentService departmentService)
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.WriteLine("--- Department Management ---");
                Console.WriteLine("1. List Departments");
                Console.WriteLine("2. Add Department");
                Console.WriteLine("3. Update Department");
                Console.WriteLine("4. Delete Department");
                Console.WriteLine("0. Back");
                Console.Write("Select an option: ");
                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        var departments = departmentService.GetAllDepartments();
                        Console.WriteLine("\nDepartments:");
                        foreach (var d in departments)
                        {
                            Console.WriteLine($"ID: {d.DepartmentId}, Name: {d.Name}");
                        }
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                    case "2":
                        Console.Write("Department Name: ");
                        var name = Console.ReadLine();
                        var newDept = new Department { Name = name };
                        departmentService.AddDepartment(newDept);
                        Console.WriteLine("Department added. Press any key to continue...");
                        Console.ReadKey();
                        break;
                    case "3":
                        Console.Write("Department ID to update: ");
                        if (int.TryParse(Console.ReadLine(), out int updateId))
                        {
                            var dept = departmentService.GetDepartmentById(updateId);
                            if (dept != null)
                            {
                                Console.Write($"Name ({dept.Name}): ");
                                var n = Console.ReadLine();
                                if (!string.IsNullOrWhiteSpace(n)) dept.Name = n;
                                departmentService.UpdateDepartment(dept);
                                Console.WriteLine("Department updated. Press any key to continue...");
                            }
                            else
                            {
                                Console.WriteLine("Department not found.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid ID.");
                        }
                        Console.ReadKey();
                        break;
                    case "4":
                        Console.Write("Department ID to delete: ");
                        if (int.TryParse(Console.ReadLine(), out int delId))
                        {
                            departmentService.DeleteDepartment(delId);
                            Console.WriteLine("Department deleted (if existed). Press any key to continue...");
                        }
                        else
                        {
                            Console.WriteLine("Invalid ID.");
                        }
                        Console.ReadKey();
                        break;
                    case "0":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Press any key to try again.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
} 