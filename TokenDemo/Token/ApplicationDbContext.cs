using Microsoft.EntityFrameworkCore;

namespace TokenDemo.Token
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions) { }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }

    
}
