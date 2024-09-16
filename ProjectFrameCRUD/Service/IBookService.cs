using ProjectFrameCRUD.Model.ResponseModel;
using ProjectFrameCRUD.Model;
using ProjectFrameCRUD.Model.RequestModel;
namespace ProjectFrameCRUD.Service
{
    public interface IBookService
    {
        public Task<bool> RegisterBook(APIRequestModel apiRequestDTO);
        public Task<APIResponseModel> FindBookById(APIRequestModel apiRequestDTO);
        public Task<bool> UpdateBook(APIRequestModel apiRequestDTO);
        public Task<bool> DeleteBook(APIRequestModel apiRequestDTO);
        public Task<APIResponseModel> GetAllBook();
    }
}
