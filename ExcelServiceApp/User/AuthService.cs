using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ExcelServiceApp.User.JWT;
using ExcelServiceApp.User.Model;
using ExcelServiceApp.User.Repository;
using Microsoft.IdentityModel.Tokens;

namespace ExcelServiceApp.User;

public class AuthService(UserRepository userRepository,IJwtService jwtService,IConfiguration configuration) : IAuthService
{
    private readonly int _accessTokenExpiationTime = Convert.ToInt32(configuration["JWT:accessTokenExpireTimeInMinutes"]); 
    private readonly int _refreshTokenExpiationTime = Convert.ToInt32(configuration["JWT:refreshTokenExpireTimeInMinutes"]); 
    private readonly string _secretKey = configuration["JWT:secretKey"];
    private readonly string _issuer = configuration["JWT:issuer"];
    private readonly string _audience = configuration["JWT:audience"];
    
    public async Task<bool> RegisterUserAsync(UserAuthApiRequest userRegisterApiRequest)
    {
        var hashSalt = HashPassword(userRegisterApiRequest.Password);
        var user = new User
        {
            Name = userRegisterApiRequest.Username,
            Email = userRegisterApiRequest.Email,
            PasswordHash = hashSalt.Item1,
            PasswordSalt = hashSalt.Item2
        };
        return await userRepository.AddAsync(user);
    }

    public async Task<UserAuthResponse> LoginUserAsync(UserAuthApiRequest userLoginApiRequest)
    {
        if (!await ValidateUser(userLoginApiRequest)) return new UserAuthResponse();
        var accessToken = jwtService.GenerateToken(_accessTokenExpiationTime,userLoginApiRequest.Email,GetAuthenticationModel());
        var refreshToken = jwtService.GenerateToken(_refreshTokenExpiationTime,userLoginApiRequest.Email,GetAuthenticationModel());
        return new UserAuthResponse()
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
        };
    }

    public async Task<UserAuthResponse> RefreshToken(string refreshToken)
    {
        var claims = ValidateRefreshToken(refreshToken);
        if (claims != null)
        {
            var userEmail = claims.Claims.FirstOrDefault(c=>c.Type==ClaimTypes.Email)?.Value;
            return new UserAuthResponse()
            {
                AccessToken = jwtService.GenerateToken(_accessTokenExpiationTime, userEmail, GetAuthenticationModel()),
                RefreshToken = jwtService.GenerateToken(_refreshTokenExpiationTime, userEmail, GetAuthenticationModel())
            };
        }
        return new UserAuthResponse();
    }

    private (byte[],byte[]) HashPassword(string password)
    {
        var hmac = new System.Security.Cryptography.HMACSHA512();
        var salt = hmac.Key;
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return (hash, salt);
    }

    private bool VerifyPassword(string password, byte[] salt, byte[] hash)
    {
        using var hmac = new System.Security.Cryptography.HMACSHA512(salt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(hash);
    }

    private async Task<bool> ValidateUser(UserAuthApiRequest userLoginApiRequest)
    {
        var found = await userRepository.FindByEmailAsync(userLoginApiRequest.Email);
       return VerifyPassword(userLoginApiRequest.Password,found.PasswordSalt,found.PasswordHash); 
    }

    private AuthenticationModel GetAuthenticationModel()
    {
        return new AuthenticationModel()
        {
            Audience = _audience,
            Issuer = _issuer,
            SecretKey = _secretKey,
        };
    }

    private ClaimsPrincipal ValidateRefreshToken(string refreshToken)
    {
        var handler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_secretKey);
        var validation = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
        var principal = handler.ValidateToken(refreshToken, validation,out var validToken);
        if (validToken is JwtSecurityToken jwtSecurityToken)
        {
            return principal;
        }
        return null;
    }
    
}