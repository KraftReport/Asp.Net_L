using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ExcelServiceApp.User.Model;
using Microsoft.IdentityModel.Tokens;

namespace ExcelServiceApp.User.JWT;

public class JwtService : IJwtService
{
    public string GenerateToken(int expireMinutes, string email,AuthenticationModel authenticationModel)
    {
        var handler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(authenticationModel.SecretKey);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, email) }),
            Expires = DateTime.UtcNow.AddMinutes(expireMinutes),
            Issuer = authenticationModel.Issuer,
            Audience = authenticationModel.Audience,
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }
}