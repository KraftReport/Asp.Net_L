namespace PizzaApiWithRedis.Security.Service;

public interface IJwtService
{
    public string generateJwtToken(string userEmail,string userId);
}