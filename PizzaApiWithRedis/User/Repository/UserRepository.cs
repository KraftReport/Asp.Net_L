using Microsoft.EntityFrameworkCore;
using PizzaApiWithRedis.Database;

namespace PizzaApiWithRedis.User.Repository
{
    public class UserRepository
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly DbSet<UserEntity> users;
        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
            this.users = applicationDbContext.Users;
        }

        public async Task<bool> AddUser(UserEntity user)
        {
            await this.users.AddAsync(user) ;
            return await applicationDbContext.SaveChangesAsync() > 0;
        }
    }
}
