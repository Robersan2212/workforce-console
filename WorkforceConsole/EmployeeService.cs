using System;
using System.Collections.Generic;
using System.Linq;

namespace WorkforceConsole
{
    public class EmployeeService
    {
        private readonly WorkforceContext _context;

        public EmployeeService(WorkforceContext context)
        {
            _context = context;
        }

        // Create a new employee
        public void AddEmployee(Employee employee)
        {
            try
            {
                // Validate department exists if specified
                if (employee.DepartmentId.HasValue)
                {
                    var department = _context.Departments.FirstOrDefault(d => d.DepartmentId == employee.DepartmentId.Value);
                    if (department == null)
                    {
                        Console.WriteLine($"Error: Department with ID {employee.DepartmentId.Value} does not exist.");
                        return;
                    }
                }

                _context.Employees.Add(employee);
                _context.SaveChanges();
                Console.WriteLine("Employee added successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding employee: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Details: {ex.InnerException.Message}");
                }
            }
        }

        // Get all employees
        public List<Employee> GetAllEmployees()
        {
            try
            {
                return _context.Employees.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving employees: {ex.Message}");
                return new List<Employee>();
            }
        }

        // Get employee by ID
        public Employee GetEmployeeById(int id)
        {
            try
            {
                return _context.Employees.FirstOrDefault(e => e.EmployeeId == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving employee: {ex.Message}");
                return null;
            }
        }

        // Update employee
        public void UpdateEmployee(Employee employee)
        {
            try
            {
                _context.Employees.Update(employee);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating employee: {ex.Message}");
            }
        }

        // Delete employee
        public void DeleteEmployee(int id)
        {
            try
            {
                var employee = _context.Employees.FirstOrDefault(e => e.EmployeeId == id);
                if (employee != null)
                {
                    _context.Employees.Remove(employee);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting employee: {ex.Message}");
            }
        }
    }
} 