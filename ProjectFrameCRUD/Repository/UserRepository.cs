using Microsoft.EntityFrameworkCore;
using ProjectFrameCRUD.Data; 
using ProjectFrameCRUD.Model.RequestModel;

namespace ProjectFrameCRUD.Repository
{
    public class UserRepository
    {
        private readonly AppDbContext appDbContext;
        private readonly DbSet<User> users;
        public UserRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
            this.users = this.appDbContext.Users;
        }

        public async Task<bool> RegisterUser(User user)
        {
            await this.users.AddAsync(user);
            return await appDbContext.SaveChangesAsync() > 0; 
        }

        public async Task<User> SearchUserById(int id)
        {
            return await users.FindAsync(id);
        }

        public async Task<List<User>> GetUsers()
        {
            return await users.ToListAsync();
        }

        public async Task<bool> UpdateUser(APIRequestModel userModel)
        {
            var found = await SearchUserById(userModel.Id);
            found.Username = userModel.User.Username;
            found.Password = userModel.User.Password;
            found.Email = userModel.User.Email;
            return await appDbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var found = await SearchUserById(id);
            users.Remove(found);
            return await appDbContext.SaveChangesAsync() > 0;
        }

        public async Task<User> SearchByEmailAndPassword(string password, string email)
        {
            return await users.Where(u=>u.Email== email && u.Password == password).FirstOrDefaultAsync();
        }

        public async Task<User> SearchByEmail(string email)
        {
            return await users.Where(u=>u.Email== email).FirstOrDefaultAsync();
        }


    }
}
