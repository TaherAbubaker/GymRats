using Microsoft.EntityFrameworkCore;
using GymRats.Models;

namespace GymRats.Data
{
    // Shared file — do NOT edit alone without telling the team.
    // If you add a new table/model, add its DbSet here.
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Name = "Admin",
                Email = "admin@gmail.com",
                PasswordHash = "admin123",
                IsAdmin = true
            });
        }
    }
}
