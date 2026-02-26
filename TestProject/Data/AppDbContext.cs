using Microsoft.EntityFrameworkCore;
using TestProject.Models.Entities;

namespace TestProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users => Set<User>();
        public DbSet<OtpLog> OtpLogs => Set<OtpLog>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.ICNumber).IsUnique();
        }
    }
}
