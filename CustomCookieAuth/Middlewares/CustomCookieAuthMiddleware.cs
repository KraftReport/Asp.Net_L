using CustomCookieAuth.Entities;
using System.Security.Claims; 
using System.Text.Json; 

namespace CustomCookieAuth.Middlewares
{
    public class CustomCookieAuthMiddleware : IMiddleware
    {
        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if(context.Request.Cookies.TryGetValue("AuthCookie",out var authCookie))
            {
                var userClaims = JsonSerializer.Deserialize<UserClaims>(authCookie);
                if(userClaims != null)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name,userClaims.Email),
                        new Claim(ClaimTypes.Role,userClaims.Role.ToString())
                    };

                    var claimsIdentity = new ClaimsIdentity(claims,"CustomCookieAuth");
                    context.User = new ClaimsPrincipal(claimsIdentity);
                }
            }
            return next(context);
        }
    }
}
