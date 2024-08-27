using Microsoft.EntityFrameworkCore;
using PlateDirectPaymentApi.Database;
using PlateDirectPaymentApi.DirectPaymentModule.Entity;

namespace PlateDirectPaymentApi.DirectPaymentModule.Repository
{
    public class MemberRepository
    {
        private readonly ApplicationDbContext applicationDbContext;
        public MemberRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;

            AddMember = async (member) =>
            {
                await applicationDbContext.Members.AddAsync(member);
                await applicationDbContext.SaveChangesAsync();
                return true;
            };

            GetMemberList = async () =>
            {
                return await applicationDbContext.Members.ToListAsync();
            };
        }

        public Func<Member, Task<bool>> AddMember { get; }

        public Func<Task<List<Member>>> GetMemberList { get; }


    }
}
