namespace Employees.Models
{
    public class AdminUser
    {
        public int Id { get; set; } // Esto es útil si lo usas con base de datos
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } // Don't store the password as plain text
        public string Email { get; set; } = string.Empty;
    }
}