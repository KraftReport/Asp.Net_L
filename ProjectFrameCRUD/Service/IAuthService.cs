using ProjectFrameCRUD.Data;
using ProjectFrameCRUD.Model;
using ProjectFrameCRUD.Model.ResponseModel;

namespace ProjectFrameCRUD.Service
{
    public interface IAuthService
    {
        public Task<Result<APIResponseModel>> Login(string email, string password);
        public Task<Result<APIResponseModel>> Register(UserModel user);
        public Task<Result<APIResponseModel>> RefreshToken(string token);
    }
}
