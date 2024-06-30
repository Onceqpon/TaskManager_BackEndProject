using Microsoft.EntityFrameworkCore;
using Core.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class TaskDbContext : IdentityDbContext<UserEntity, UserRole, int>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }

        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Konfiguracja dodatkowych właściwości, relacji itp.
        }
    }
}
