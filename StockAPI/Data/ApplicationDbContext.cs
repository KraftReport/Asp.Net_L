using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StockAPI.Model;
using System.Net.Sockets;

namespace StockAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }
        
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Portfolio>(x => x.HasKey(p => new { p.AppUserId, p.StockId }));
            modelBuilder.Entity<Portfolio>()
                .HasOne(p => p.Stock)
                .WithMany(p => p.Portfolios)
                .HasForeignKey(p => p.StockId);
            modelBuilder.Entity<Portfolio>()
                .HasOne(p => p.appUser)
                .WithMany(p => p.Portfolios)
                .HasForeignKey(p => p.AppUserId);
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "user",
                    NormalizedName = "USER"
                }
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
