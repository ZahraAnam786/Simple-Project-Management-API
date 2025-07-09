using Microsoft.EntityFrameworkCore;
using Simple_Project_Management_API.Models;
using static Azure.Core.HttpHeader;

namespace Simple_Project_Management_API.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
