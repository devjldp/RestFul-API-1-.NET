using System.ComponentModel.DataAnnotations;

namespace Employees.Models
{
    public class AdminUser
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string PasswordHash { get; set; } // Don't store the password as plain text
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}