using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employees.Models
{
    public class Employee
    {
        public int Id { get; set;}
        [Required]
        [Column("first_name")]
        public string FirstName { get; set;} = string.Empty;
        
        [Column("last_name"), Required] // Different syntax
        public string LastName { get; set;} = string.Empty;
        [Required]
        [Column("email")]
        public string Email { get; set;} = string.Empty;
        [Required]
        [Column("city")]
        public string City { get; set;} = string.Empty;
        [Required]
        [Column("role")]
        public string Role{ get; set;} = string.Empty;
    }
}