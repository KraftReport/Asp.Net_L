using CookieBasedAuthDemo.CookieAuth; 
using Microsoft.EntityFrameworkCore;

namespace CookieBasedAuthDemo.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Product.Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
/*
            modelBuilder.Entity<AppUser>().HasKey(k => k.Id);*/
        }
    }
}
