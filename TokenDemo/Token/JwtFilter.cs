using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers; 
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace TokenDemo.Token
{
    public class JwtFilter : ActionFilterAttribute
    {
        private TokenService tokenService;
        private string SECRET = "thisisjwtsymmetrickeyforjwttokengeneration";

        public JwtFilter(TokenService tokenService)
        {
            this.tokenService = tokenService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var token = context.HttpContext.Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var tokenHandler = new JwtSecurityTokenHandler();

            if (token == null || token == "")
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Result = new Microsoft.AspNetCore.Mvc.JsonResult(new
                { Message = $"Token validation failed:  token is not found in authorization header " });
                return;
            }

            if (!tokenHandler.CanReadToken(token))
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Result = new Microsoft.AspNetCore.Mvc.JsonResult(new
                { Message = $"Token validation failed:  something wrong with reading token make sure your token is correct " });
                return;
            }

            var jwt = tokenHandler.ReadToken(token);

            if (tokenService.ValidateTokenExpirationTime(token) == false)
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Result = new Microsoft.AspNetCore.Mvc.JsonResult(new
                { Message = $"Token validation failed:  access token is expired " });
                return;
            }

            if (jwt.Issuer != "myosetpaing")
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Result = new Microsoft.AspNetCore.Mvc.JsonResult(new
                { Message = $"Token validation failed:  token issuer is not consistant with specified one " });
                return;
            }

            var tokenParts = token.Split(".");

            if (tokenParts.Length != 3)
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Result = new Microsoft.AspNetCore.Mvc.JsonResult(new
                { Message = $"Token validation failed:  token structure is not consistant with jwt token " });
                return;
            }

            var header = tokenParts[0];
            var payload = tokenParts[1];
            var signature = tokenParts[2];

            var singedContent = $"{header}.{payload}";

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET));

            using (var hmac = new HMACSHA256(key.Key))
            {
                var computedSingnatureBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(singedContent));
                var computetSignature = Base64UrlEncoder.Encode(computedSingnatureBytes);
                if (computetSignature != signature)
                {
                    context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Result = new Microsoft.AspNetCore.Mvc.JsonResult(new
                    { Message = $"Token validation failed:  token is failed to authenticate symmetric key" });
                    return;
                }
            }
        }


    }
}
