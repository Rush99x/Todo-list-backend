using Microsoft.EntityFrameworkCore;
using Todo_List_Project.Models;

namespace Todo_List_Project.Data
{
    // AppDbContext class inherits from DbContext provided by Entity Framework Core
    public class AppDbContext : DbContext
    {
        // Constructor to initialize AppDbContext with options
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        //DbSet representing the Todo,User tables in the database
        public DbSet<Todo> Todo { get; set; }
        public DbSet<Users> User { get; set; }
    }

}

