using Microsoft.EntityFrameworkCore;
using ProjectFrameCRUD.Data;

namespace ProjectFrameCRUD.Repository
{
    public class TokenRepository
    {
        private readonly AppDbContext appDbContext;
        private readonly DbSet<Token> tokens;

        public TokenRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
            this.tokens = this.appDbContext.Tokens;
        }

        public async Task<bool> SaveToken(Token token)
        {
            await tokens.AddAsync(token);
            return await appDbContext.SaveChangesAsync() > 0;
        }

        public async Task<Token> FindByUserId(int id)
        {
            return await tokens.Where(t => t.UserId == id).FirstOrDefaultAsync();
        }

        public async Task<Token> FindByToken(string token)
        {
            return await tokens.Where(t => t.AccessToken == token).FirstOrDefaultAsync();   
        }

        public async Task<bool> UpdateToken(string token,int id)
        {
            var found = await tokens.FindAsync(id);
            found.AccessToken = token;
            return await appDbContext.SaveChangesAsync() > 0;
        } 

        public async Task<bool> DeleteToken(int userId)
        {
            var found = await FindByUserId(userId);
            tokens.Remove(found);
            return await appDbContext.SaveChangesAsync() > 0;
        }
    }
}
