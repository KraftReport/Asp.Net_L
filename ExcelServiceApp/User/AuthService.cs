using System.Text;
using ExcelServiceApp.User.JWT;
using ExcelServiceApp.User.Model;
using ExcelServiceApp.User.Repository;

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

    public Task<string> RefreshToken(string refreshToken)
    {
        throw new NotImplementedException();
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
       // var valid = VerifyPassword(userLoginApiRequest.Password,found.PasswordSalt,found.PasswordHash);
        return true;
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
}