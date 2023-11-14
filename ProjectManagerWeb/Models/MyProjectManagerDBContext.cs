using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using ProjectManagerWeb.Models;

namespace ProjectManagerWeb.Models
{
    public class MyProjectManagerDBContext : DbContext
    {
        public virtual DbSet<Project> Projects { get; set; }

        public virtual DbSet<Todo> Todo { get; set; }
        public DbSet<User> User { get; set; }

        public MyProjectManagerDBContext(DbContextOptions<MyProjectManagerDBContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        

        
    }
}
