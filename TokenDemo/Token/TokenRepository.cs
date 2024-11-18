using Microsoft.EntityFrameworkCore; 
using System.Threading.Tasks;

namespace TokenDemo.Token
{
    public class TokenRepository
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly DbSet<RefreshToken> refreshTokens;
        public TokenRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
            refreshTokens = this.applicationDbContext.RefreshTokens;
        }

        public async Task<bool> SaveRefreshToken(RefreshToken refreshToken)
        {
            await refreshTokens.AddAsync(refreshToken);
            return await applicationDbContext.SaveChangesAsync() > 0;
        }

        public async Task<RefreshToken> FindRefreshToken(string code)
        {
            var found = await refreshTokens.FirstOrDefaultAsync(t=>t.code.Equals(code));

            if (found != null)
                return found; 
            return null;
        }

        public async Task<bool> UpdateRefreshToken(RefreshToken refreshToken)
        {
            var found = await refreshTokens.FirstOrDefaultAsync(d=>d.id.Equals(refreshToken.id));

            if (found == null) return false; 

            found.code = refreshToken.code;
            found.expirationTime = refreshToken.expirationTime;

            await applicationDbContext.SaveChangesAsync();

            return true; 
        }
    }
}
