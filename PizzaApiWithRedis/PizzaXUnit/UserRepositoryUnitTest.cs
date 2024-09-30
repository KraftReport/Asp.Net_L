using Microsoft.EntityFrameworkCore;
using Moq;
using PizzaApiWithRedis.Database;
using PizzaApiWithRedis.User;
using PizzaApiWithRedis.User.Repository;

namespace PizzaApiWithRedis.Test.User.UnitTest;

public class UserRepositoryUnitTest : IClassFixture<DatabaseFixture>, IAsyncLifetime
{


    private readonly Mock<ApplicationDbContext> applicationDbContext;
    private readonly Mock<DbSet<UserEntity>> users;
    private readonly DatabaseFixture databaseFixture;
    private readonly UserRepository userRepository;
    private UserEntity user;

    public UserRepositoryUnitTest(DatabaseFixture databaseFixture)
    {
        this.applicationDbContext = new Mock<ApplicationDbContext>();
        this.users = new Mock<DbSet<UserEntity>>();
        this.databaseFixture = databaseFixture;
    }


    Task IAsyncLifetime.DisposeAsync()
    {
        applicationDbContext.Reset();

        return Task.CompletedTask;
    }

    Task IAsyncLifetime.InitializeAsync()
    {
        user = new UserEntity
        {
            Id = 2,
            Email = "myosetpaing@gmail.com",
            Name = "myosetpaing",
            Password = "123"
        };

        return Task.CompletedTask;
    }

    [Fact]
    public async Task AddProductAsync_ShouldAddProduct()
    {
        await userRepository.AddUser(user);

        users.Verify(m => m.AddAsync(It.IsAny<UserEntity>(), It.IsAny<CancellationToken>()), Times.Once);
        applicationDbContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }



}