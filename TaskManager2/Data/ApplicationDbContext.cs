using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManager2.Models;

namespace TaskManager2.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<TaskManager2.Models.TaskStatusLabel>? TaskStatusLabel { get; set; }
        public DbSet<TaskManager2.Models.WorkTask>? WorkTask { get; set; }
    }
}