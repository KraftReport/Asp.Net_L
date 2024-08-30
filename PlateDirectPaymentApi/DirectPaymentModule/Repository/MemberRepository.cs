using Microsoft.EntityFrameworkCore;
using PlateDirectPaymentApi.Database;
using PlateDirectPaymentApi.DirectPaymentModule.Entity;
using PlateDirectPaymentApi.DirectPaymentModule.Model;

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


        public async Task<bool> UpdateMember(int Id,MemberDTO memberDTO)
        {
            var found = await applicationDbContext.Members.FindAsync(Id);
            found.Email = memberDTO.Email;
            found.IsDeleted = false;
            found.Name = memberDTO.Name;
            return await applicationDbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteMember(int Id)
        {
            var found = await applicationDbContext.Members.FindAsync(Id);
            found.IsDeleted = true;
            return await applicationDbContext.SaveChangesAsync() > 0;
        }
      
        public async Task<Member> findById(int id)
        {
            return await applicationDbContext.Members.FindAsync(id);
        }

    }
}
