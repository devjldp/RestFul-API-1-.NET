using Microsoft.EntityFrameworkCore;

// import models
using Employees.Models;


namespace Employees.Data{
    public class AppDbContext : DbContext
    {
        // models
        public DbSet<Employee> Employees { get; set;}
        public DbSet<AdminUser> AdminUsers{get; set;}
    
        // Constructor that receive the configurations options 
        // and passes them to the base DbContext class.
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
    }
}