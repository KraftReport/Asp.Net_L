using ProjectFrameCRUD.Data;
using ProjectFrameCRUD.Model;
using ProjectFrameCRUD.Model.ResponseModel;
using ProjectFrameCRUD.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ProjectFrameCRUD.Service
{
    public class AuthService : IAuthService
    {
        private readonly TokenRepository _tokenRepository;
        private readonly UserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public AuthService(TokenRepository tokenRepository, UserRepository userRepository, IJwtService jwtService)
        {
            _tokenRepository = tokenRepository;
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public async Task<Result<APIResponseModel>> Register(UserModel user)
        {
            var mappedUser = EntityMapper(user);
            await _userRepository.RegisterUser(mappedUser);
            return Result<APIResponseModel>.Success("User registered successfully", new APIResponseModel());
        }

        public async Task<Result<APIResponseModel>> Login(string email, string password)
        {
            var user = await _userRepository.SearchByEmailAndPassword(password, email);
            if (user == null)
            {
                return Result<APIResponseModel>.Fail("Invalid email or password");
            }

            var accessToken = _jwtService.GenerateToken(user, SecretKey.Key, 1, "user");
            var refreshToken = _jwtService.GenerateToken(user, SecretKey.Key, 5, "user");

            var newToken = new Token
            {
                AccessToken = accessToken,
                CreatedAt = DateTime.UtcNow,
                UserId = user.Id
            };
            await _tokenRepository.SaveToken(newToken);

            return Result<APIResponseModel>.Success("Login successful", new APIResponseModel
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }

        public async Task<Result<APIResponseModel>> RefreshToken(string token)
        {
            var isValid = _jwtService.ValidateToken(token, SecretKey.Key);
            if (isValid == SecurityEnum.INVALID)
            {
                return Result<APIResponseModel>.Fail("Invalid refresh token");
            }

            if (isValid == SecurityEnum.EXPIRED)
            {
                return Result<APIResponseModel>.Fail("Refresh token expired, please log in again");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwt = tokenHandler.ReadJwtToken(token);
            var email = jwt.Claims.FirstOrDefault(j => j.Type == ClaimTypes.Email)?.Value;
            var user = await _userRepository.SearchByEmail(email);

            if (user == null)
            {
                return Result<APIResponseModel>.Fail("User not found");
            }

            var foundToken = await _tokenRepository.FindByUserId(user.Id);

            var newAccessToken = _jwtService.GenerateToken(user, SecretKey.Key, 1, "user");
            var newRefreshToken = _jwtService.GenerateToken(user, SecretKey.Key, 5, "user");

            await _tokenRepository.UpdateToken(newAccessToken, foundToken.Id);

            return Result<APIResponseModel>.Success("Tokens refreshed successfully", new APIResponseModel
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }

        private User EntityMapper(UserModel model)
        {
            return new User
            {
                CreatedAt = DateTime.UtcNow,
                Email = model.Email,
                Password = model.Password,
                Username = model.Username
            };
        }
    }
}
