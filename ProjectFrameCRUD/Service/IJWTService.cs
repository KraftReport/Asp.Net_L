using ProjectFrameCRUD.Data;
using ProjectFrameCRUD.Model;

namespace ProjectFrameCRUD.Service
{
    public interface IJwtService
    {
        public string GenerateToken(User user, string secret, int timestamp, string role);
        public SecurityEnum ValidateToken(string token,string secret);
    }
}
