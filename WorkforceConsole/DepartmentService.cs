using System;
using System.Collections.Generic;
using System.Linq;

namespace WorkforceConsole
{
    public class DepartmentService
    {
        private readonly WorkforceContext _context;

        public DepartmentService(WorkforceContext context)
        {
            _context = context;
        }

        // Create a new department
        public void AddDepartment(Department department)
        {
            try
            {
                _context.Departments.Add(department);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding department: {ex.Message}");
            }
        }

        // Get all departments
        public List<Department> GetAllDepartments()
        {
            try
            {
                return _context.Departments.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving departments: {ex.Message}");
                return new List<Department>();
            }
        }

        // Get department by ID
        public Department GetDepartmentById(int id)
        {
            try
            {
                return _context.Departments.FirstOrDefault(d => d.DepartmentId == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving department: {ex.Message}");
                return null;
            }
        }

        // Update department
        public void UpdateDepartment(Department department)
        {
            try
            {
                _context.Departments.Update(department);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating department: {ex.Message}");
            }
        }

        // Delete department
        public void DeleteDepartment(int id)
        {
            try
            {
                var department = _context.Departments.FirstOrDefault(d => d.DepartmentId == id);
                if (department != null)
                {
                    _context.Departments.Remove(department);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting department: {ex.Message}");
            }
        }
    }
} 