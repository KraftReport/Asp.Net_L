using PlateDirectPaymentApi.DirectPaymentModule.Entity;
using PlateDirectPaymentApi.DirectPaymentModule.Model;
using PlateDirectPaymentApi.DirectPaymentModule.Repository;

namespace PlateDirectPaymentApi.DirectPaymentModule.Service
{
    public class MemberService
    {
        private readonly MemberRepository memberRepository;

        public MemberService(MemberRepository memberRepository)
        {
            this.memberRepository = memberRepository;


            RegisterMember = async (memberDTO) =>
            {
                await memberRepository.AddMember(memberMapper(memberDTO));
                return true;
            };

            GetMemberList = async () =>
            {
                return await memberRepository.GetMemberList();
            };

            memberMapper = (memberDTO) =>
            {
                return new Member
                {
                    Name = memberDTO.Name,
                    Email = memberDTO.Email
                };
            };

            dtoMapper = (member) =>
            {
                return new MemberDTO
                {
                    Name = member.Name,
                    Email = member.Email,
                };
            };

        }

        public Func<Task<List<Member>>> GetMemberList { get; }

        public Func<MemberDTO, Task<bool>> RegisterMember { get; }

        private Func<MemberDTO, Member> memberMapper { get; }

        private Func<Member, MemberDTO> dtoMapper { get; }

    }
}
