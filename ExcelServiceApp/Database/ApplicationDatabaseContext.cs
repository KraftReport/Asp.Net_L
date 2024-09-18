using Microsoft.EntityFrameworkCore;
using ExcelServiceApp.User;

namespace ExcelServiceApp.Database;

public class ApplicationDatabaseContext(DbContextOptions<ApplicationDatabaseContext> options) : DbContext(options)
{
    public DbSet<User.User> Users { get; set; }
}