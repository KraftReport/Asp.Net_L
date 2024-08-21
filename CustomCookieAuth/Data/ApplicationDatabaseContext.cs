using CustomCookieAuth.Entities;
using CustomCookieAuth.Services;
using Microsoft.EntityFrameworkCore;

namespace CustomCookieAuth.Data
{
    public class ApplicationDatabaseContext : DbContext
    {
        public ApplicationDatabaseContext(DbContextOptions<ApplicationDatabaseContext> options) : base(options) { } 

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>().HasIndex(au=>au.Email).IsUnique();
            var (hash, salt) = PasswordHashingService.HashPassword("admin");
            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Name = "admin",
                    Id = 1,
                    Email = "admin@gmail.com",
                    Password = hash,
                    Salt = salt,
                    Role = ROLE.ADMIN
                }
            );


        }
    }
}
