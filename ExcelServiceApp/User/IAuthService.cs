using ExcelServiceApp.User.Model;

namespace ExcelServiceApp.User;

public interface IAuthService
{
    public Task<bool> RegisterUserAsync(UserAuthApiRequest userRegisterApiRequest);
    public Task<UserAuthResponse> LoginUserAsync(UserAuthApiRequest userLoginApiRequest);
    public Task<UserAuthResponse> RefreshToken(string refreshToken);
}