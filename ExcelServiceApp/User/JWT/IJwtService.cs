using ExcelServiceApp.User.Model;

namespace ExcelServiceApp.User.JWT; 

public interface IJwtService
{
    public string GenerateToken(int expireMinutes,string email,AuthenticationModel authenticationModel);   
}