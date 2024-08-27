using PlateDirectPaymentApi.DirectPaymentModule.Entity;
using PlateDirectPaymentApi.DirectPaymentModule.Model;
using PlateDirectPaymentApi.DirectPaymentModule.Repository;

namespace PlateDirectPaymentApi.DirectPaymentModule.Service
{
    public class MemberService
    {
        private readonly MemberRepository memberRepository;
        private readonly CurrencyRepository currencyRepository;

        public MemberService(MemberRepository memberRepository,CurrencyRepository currencyRepository)
        {
            this.currencyRepository = currencyRepository;
            this.memberRepository = memberRepository;


            RegisterMember = async (memberDTO) =>
            {
                await memberRepository.AddMember(memberMapper(memberDTO));
                return true;
            };

            GetMemberList = async () =>
            {
                var members = await memberRepository.GetMemberList();
                return await mapToDtoList(members);
            };

            memberMapper = (memberDTO) =>
            {
                return new Member
                {
                    Name = memberDTO.Name,
                    Email = memberDTO.Email
                };
            };

            dtoMapper = async (member) =>
            {
                var goldRecord = await currencyRepository.FindRecordByMemberId(member.Id, Enum.PlateType.GOLD);
                var silverRecord = await currencyRepository.FindRecordByMemberId(member.Id, Enum.PlateType.SILVER);
                return new MemberDTO
                {
                    Name = member.Name,
                    Email = member.Email,
                    GoldPlate = goldRecord != null ? goldRecord.PlateCount.ToString() : "0",
                    SilverPlate = silverRecord != null ? silverRecord.PlateCount.ToString() : "0"
                };
            };

            mapToDtoList = async (members) =>
            {
                var memberDTOs = new List<MemberDTO>();

                foreach (var member in members)
                {
                    var dto = await dtoMapper(member);
                    memberDTOs.Add(dto);
                }

                return memberDTOs;
            };

        }

        public Func<Task<List<MemberDTO>>> GetMemberList { get; }

        public Func<MemberDTO, Task<bool>> RegisterMember { get; }

        private Func<MemberDTO, Member> memberMapper { get; }

        private Func<Member, Task<MemberDTO>> dtoMapper { get; }

        private Func<List<Member>,Task<List<MemberDTO>>> mapToDtoList { get; }

    }
}
