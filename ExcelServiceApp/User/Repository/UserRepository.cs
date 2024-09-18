using ExcelServiceApp.Database;
using Microsoft.EntityFrameworkCore;

namespace ExcelServiceApp.User.Repository;

public class UserRepository
{
    private readonly ApplicationDatabaseContext _applicationDatabaseContext;
    private readonly DbSet<User> _users;

    public UserRepository(ApplicationDatabaseContext applicationDatabaseContext)
    {
        _applicationDatabaseContext = applicationDatabaseContext;
        _users = _applicationDatabaseContext.Users;
    }

    public async Task<bool> AddAsync(User user)
    {
        _users.Add(user);
        return await _applicationDatabaseContext.SaveChangesAsync() > 0;
    }

    public async Task<User> FindByEmailAsync(string email)
    {
        return (await _applicationDatabaseContext.Users.FirstOrDefaultAsync(u => u.Email == email))!;
    }

    public async Task<User> FindByEmailAndPassword(string email, byte[] password)
    {
        return (await _applicationDatabaseContext.Users.FirstOrDefaultAsync(u=>u.PasswordHash == password && u.Email == email))!;
    }
}