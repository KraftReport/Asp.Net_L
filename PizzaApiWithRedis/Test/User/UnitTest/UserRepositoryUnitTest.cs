using Microsoft.EntityFrameworkCore;
using Moq;
using PizzaApiWithRedis.Database;
using PizzaApiWithRedis.User;
using PizzaApiWithRedis.User.Repository;
using Xunit;

namespace PizzaApiWithRedis.Test.User.UnitTest;

public class UserRepositoryUnitTest
{
    private readonly Mock<ApplicationDbContext> _mockDbContext;
    private readonly Mock<DbSet<UserEntity>> _mockDbSet;
    private readonly UserRepository userRepository;

    public UserRepositoryUnitTest()
    {
        _mockDbContext = new Mock<ApplicationDbContext>();
        _mockDbSet = new Mock<DbSet<UserEntity>>();
        _mockDbContext.Setup(m=>m.Users).Returns(_mockDbSet.Object);
        userRepository = new UserRepository(_mockDbContext.Object);
    }

    [Fact]
    public async Task AddProductAsync_ShouldAddProduct()
    {

    }
}