using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TokenDemo.Token;

namespace TokenDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly TokenService _tokenService;
        public TokenController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("generate-access-token")]
        public IActionResult GenerateAccessToken()
        {
            return Ok(_tokenService.GenerateAccessToken());
        }

        [HttpPost]
        [Route("validate-expiration-time")]
        public IActionResult ValidateExpirationTime([FromBody]string token)
        {
            return Ok(_tokenService.ValidateTokenExpirationTime(token));
        }

        [HttpPost]
        [Route("get-claims-from-token")]
        public IActionResult GetClaimsFromToken([FromBody]string token)
        {
            return Ok(_tokenService.GetClaimsFromAccessToken(token));
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody]string token)
        {
            return Ok(await _tokenService.RefreshTokenProcess(token));
        }

        [HttpPost]
        [Route("generate-refresh-token")]
        public async Task<IActionResult> GenerateRefreshToken()
        {
            return Ok(await _tokenService.GenerateRefreshToken());
        }

        [HttpPost]
        [ServiceFilter(typeof(JwtFilter))]
        [Route("this-is-secure-endpoint")]
        public IActionResult SecureEndpoint()
        {
            return Ok("this is secure endpoint");
        }
    }
}
