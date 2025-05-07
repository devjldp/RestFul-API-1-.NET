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
        
        // methods

        [HttpPost("register")]
        public IActionResult RegisterAdmin([FromBody] AdminUser newAdmin){
            // Check if a user with the same username or email already exists
            var existingUser = _context.AdminUsers.FirstOrDefault(u =>
            u.UserName == newAdmin.UserName || u.Email == newAdmin.Email);

            if (existingUser != null)
            {
                // returns a 400 BadRequest 
                return BadRequest("User with the same username or email already exists.");
            }

            // Hash the plain text password using BCrypt
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newAdmin.PasswordHash);

            newAdmin.PasswordHash = hashedPassword;
            
            // Save the new admin user to the database -> EF Core methods
            _context.AdminUsers.Add(newAdmin);
            _context.SaveChanges();

            // Returns a success response (200 OK) with a message to confirm registration.
            return Ok(new { message = "Admin registered successfully." });
        }


    }
}
