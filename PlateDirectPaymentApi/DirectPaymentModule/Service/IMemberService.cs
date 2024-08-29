using PlateDirectPaymentApi.DirectPaymentModule.Model;

namespace PlateDirectPaymentApi.DirectPaymentModule.Service
{
    public interface IMemberService
    {
        public Task<bool> RegisterMember(MemberDTO member);
        public Task<List<MemberDTO>> GetMemberList();
    }
}
