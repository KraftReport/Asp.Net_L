using Microsoft.EntityFrameworkCore;
using PlateDirectPaymentApi.DirectPaymentModule.Entity;

namespace PlateDirectPaymentApi.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Member> Members { get; set; }
        public DbSet<PlateCurrency> PlateCurrency { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
