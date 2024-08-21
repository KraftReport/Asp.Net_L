using CustomCookieAuth.Data;
using CustomCookieAuth.Entities;
using CustomCookieAuth.Services;
using Microsoft.EntityFrameworkCore;

namespace CustomCookieAuth.Repositories
{
    public class ApplicationUserRepository
    {
        private readonly ApplicationDatabaseContext _applicationDatabaseContext; 
        public ApplicationUserRepository(ApplicationDatabaseContext applicationDatabaseContext) 
        {
            _applicationDatabaseContext = applicationDatabaseContext;
        }

        public async Task createUser(ApplicationUser applicationUser)
        {
            await _applicationDatabaseContext.ApplicationUsers.AddAsync(applicationUser);
            await _applicationDatabaseContext.SaveChangesAsync();
        }

        public async Task<ApplicationUser> findById(int Id)
        {
            return await _applicationDatabaseContext.ApplicationUsers.FindAsync(Id);
        }

        public async Task<ApplicationUser> findByEmail(string email)
        {
            return await _applicationDatabaseContext.ApplicationUsers.SingleOrDefaultAsync(s => s.Email == email);
        }

        public async Task<List<ApplicationUser>> findAll()
        {
            return await _applicationDatabaseContext.ApplicationUsers.ToListAsync();
        }

        public async Task updateUser(ApplicationUser applicationUser)
        {
            var (hash,salt) = PasswordHashingService.HashPassword(applicationUser.Password);
            var user = await findById(applicationUser.Id);
            user.Role = applicationUser.Role;
            user.Email = applicationUser.Email;
            user.Salt = salt;
            user.Password = hash;
            user.Name = applicationUser.Name;
            await _applicationDatabaseContext.SaveChangesAsync();
        }

        public async Task deleteUser(int Id)
        {
            _applicationDatabaseContext.Remove(await findById(Id));
            await _applicationDatabaseContext.SaveChangesAsync();
        }
    }
}
