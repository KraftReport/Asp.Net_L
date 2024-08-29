using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlateDirectPaymentApi.DirectPaymentModule.Model;
using PlateDirectPaymentApi.DirectPaymentModule.Service;

namespace PlateDirectPaymentApi.DirectPaymentModule.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService memberService;
        public MemberController(IMemberService memberService)
        {
            this.memberService = memberService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMemberList()
        {
            return Ok(await memberService.GetMemberList());
        }

        [HttpPost]
        public async Task<IActionResult> RegisterMember([FromBody] MemberDTO memberDTO)
        {
            return Ok(await memberService.RegisterMember(memberDTO));
        }
    }
}
