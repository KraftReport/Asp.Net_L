using Microsoft.EntityFrameworkCore;
using PlateDirectPaymentApi.Database;
using PlateDirectPaymentApi.DirectPaymentModule.Entity; 

namespace PlateDirectPaymentApi.DirectPaymentModule.Repository
{
    public class MemberRepository
    {
        private readonly ApplicationDbContext applicationDbContext; 
        public MemberRepository(ApplicationDbContext applicationDbContext )
        { 
            this.applicationDbContext = applicationDbContext;
        }

      

        public async Task<bool> AddMember(Member member)
        {
            await applicationDbContext.Members.AddAsync(member); 
            return await applicationDbContext.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<List<Member>> GetMemberList()
        {
            return await applicationDbContext.Members.ToListAsync();
        }

      


    }
}
