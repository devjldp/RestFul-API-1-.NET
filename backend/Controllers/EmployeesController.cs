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
                // Acción POST: Crear un nuevo empleado
        [HttpPost]
        public Employee CreateEmployee(Employee employee)
        {
            _context.Employee.Add(employee); // Añadir el nuevo empleado
            _context.SaveChangesAsync(); // Guardar los cambios en la base de datos

            // Retorna el empleado recién creado con un código de estado 201 (Creado)
            return employee;
        }
    }

}