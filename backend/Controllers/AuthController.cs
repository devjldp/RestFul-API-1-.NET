using Microsoft.AspNetCore.Mvc;
using Employees.Data;
using Employees.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Employees.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(AppDbContext context,  IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
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

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginRequest loginRequest){
            // Step 1: Get the admin user by username or email
            var adminUser = _context.AdminUsers
                                    .FirstOrDefault(u => u.UserName == loginRequest.UserName);
            
            // Step 2: Check if the user exists and if the password is correct
            if (adminUser == null || !BCrypt.Net.BCrypt.Verify(loginRequest.Password, adminUser.PasswordHash))
            {
                return Unauthorized("Invalid username or password.");
            }
            
            // Step 3: Create JWT Token
            var token = GenerateJwtToken(adminUser);
            
            // Step 4: Return the token to the frontend
            return Ok(new { Token = token });
        }

        [HttpGet("admin")]
        public IEnumerable<AdminUser> GetAdmins(){
            return _context.AdminUsers.ToList();
        }

        private string GenerateJwtToken(AdminUser adminUser){
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]{
                new Claim(ClaimTypes.Name, adminUser.UserName),
                new Claim(ClaimTypes.NameIdentifier, adminUser.Id.ToString()),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var token = new JwtSecurityToken(
                issuer: "yourapp.com",
                audience: "yourapp.com",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
