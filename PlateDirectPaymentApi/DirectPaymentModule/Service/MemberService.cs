using PlateDirectPaymentApi.DirectPaymentModule.Entity;
using PlateDirectPaymentApi.DirectPaymentModule.Model; 
using PlateDirectPaymentApi.DirectPaymentModule.Repository;

namespace PlateDirectPaymentApi.DirectPaymentModule.Service
{
    public class MemberService : IMemberService
    {
        private readonly MemberRepository memberRepository;
        private readonly CurrencyRepository currencyRepository;

        public MemberService(MemberRepository memberRepository, CurrencyRepository currencyRepository)
        {
            this.currencyRepository = currencyRepository;
            this.memberRepository = memberRepository;
        }


        public async Task<List<MemberDTO>> GetMemberList()
        {
            var members = await memberRepository.GetMemberList();
            var filtered = members.Where(m => !m.IsDeleted).ToList();
            return await mapToDtoList(filtered);
        }

        public async Task<bool> RegisterMember(MemberDTO member)
        {
            return await memberRepository.AddMember(await memberMapper(member));
        }

        private async Task<Member> memberMapper(MemberDTO memberDTO)
        {
            return new Member
            {
                Name = memberDTO.Name,
                Email = memberDTO.Email
            };
        }

        private async Task<MemberDTO> dtoMapper(Member member)
        {

            var goldRecord = await currencyRepository.FindRecordByMemberIdAndPlateType(member.Id, Enum.PlateType.GOLD);
            var silverRecord = await currencyRepository.FindRecordByMemberIdAndPlateType(member.Id, Enum.PlateType.SILVER);
            return new MemberDTO
            {
                Name = member.Name,
                Email = member.Email,
                GoldPlate = goldRecord != null ? goldRecord.PlateCount.ToString() : "0",
                SilverPlate = silverRecord != null ? silverRecord.PlateCount.ToString() : "0"
            };
        }

        private async Task<List<MemberDTO>> mapToDtoList(List<Member> members)
        {
            var memberDTOs = new List<MemberDTO>();

            foreach (var member in members)
            {
                var dto = await dtoMapper(member);
                memberDTOs.Add(dto);
            }
            return memberDTOs;
        }

        public async Task<bool> UpdateMember(int id,MemberDTO member)
        {
            return await memberRepository.UpdateMember(id,member);
        }

        public async Task<bool> DeleteMember(int Id)
        {
            return await memberRepository.DeleteMember(Id);
        }

        public async Task<Member> findById(int id)
        {
            return await memberRepository.findById(id);
        }


    }
}
