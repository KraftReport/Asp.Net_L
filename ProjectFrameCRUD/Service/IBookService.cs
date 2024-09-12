using ProjectFrameCRUD.DTO;

namespace ProjectFrameCRUD.Service
{
    public interface IBookService
    {
        public Task<bool> RegisterBook(APIRequestDTO apiRequestDTO);
        public Task<APIResponseDTO> FindBookById(APIRequestDTO apiRequestDTO);
        public Task<bool> UpdateBook(APIRequestDTO apiRequestDTO);
        public Task<bool> DeleteBook(APIRequestDTO apiRequestDTO);
        public Task<APIResponseDTO> GetAllBook();
    }
}
