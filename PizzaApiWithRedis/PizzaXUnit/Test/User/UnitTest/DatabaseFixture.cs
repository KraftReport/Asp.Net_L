using Microsoft.EntityFrameworkCore;
using PizzaApiWithRedis.Database;
using PizzaApiWithRedis.User;

namespace PizzaApiWithRedis.Test.User.UnitTest
{
    public class DatabaseFixture : IDisposable
    {
        public ApplicationDbContext applicationDbContext { get; private set; }
        public DatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "testdb")
                .Options;

            applicationDbContext = new ApplicationDbContext(options);
        }
        public void Dispose()
        {
            applicationDbContext.Dispose();    
        }
    }
}
