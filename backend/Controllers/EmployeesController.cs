using Microsoft.AspNetCore.Mvc;
using Employees.Data;
using Employees.Models;

namespace Employees.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }


        // methods:
        // Acción POST: Add a new employee -> Post: employee/insert
        [HttpPost("insert")]
        public Employee CreateEmployee(Employee employee)
        {
            _context.Employees.Add(employee); // Add a new employee
            _context.SaveChangesAsync(); // Save changes in database

            // Return the employee
            return employee;
        }

        // Get all employees
        [HttpGet]
        public IEnumerable<Employee> GetEmployees(){
            return _context.Employees.ToList();
        }
    }

}