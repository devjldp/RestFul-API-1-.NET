using Microsoft.AspNetCore.Mvc;
using Employees.Data;
using Employees.Models;

namespace Employees.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }
        
        // Aquí irá el método de registro





    }
}
