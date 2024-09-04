using Microsoft.EntityFrameworkCore;

namespace PizzaApiWithRedis.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Pizza.Model.PizzaDetail> pizza { get; set; }
    }
}
