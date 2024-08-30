using PlateDirectPaymentApi.DirectPaymentModule.Entity;
using PlateDirectPaymentApi.DirectPaymentModule.Model;

namespace PlateDirectPaymentApi.DirectPaymentModule.Service
{
    public interface IMemberService
    {
        public Task<bool> RegisterMember(MemberDTO member);
        public Task<List<MemberDTO>> GetMemberList();
        public Task<bool> UpdateMember(int id,MemberDTO member);
        public Task<bool> DeleteMember(int Id);
        public Task<Member> findById(int id);
    }
}
