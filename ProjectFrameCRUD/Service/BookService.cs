using ProjectFrameCRUD.Model.ResponseModel;
using ProjectFrameCRUD.Model;
using ProjectFrameCRUD.Model.RequestModel;
using ProjectFrameCRUD.Data;
using ProjectFrameCRUD.Repository;
using System.Collections.Immutable;

namespace ProjectFrameCRUD.Service
{
    public class BookService : IBookService
    {
        private readonly BookRepository bookRepository;
        public BookService(BookRepository bookRepository)
        {
            this.bookRepository = bookRepository;   
        }

        public async Task<bool> RegisterBook(APIRequestModel apiRequestDTO)
        {
            return await bookRepository.SaveBook(EntityMapper(apiRequestDTO.Book));
        }

        public async Task<APIResponseModel> FindBookById(APIRequestModel apiRequestDTO)
        {
            return new APIResponseModel
            {
                Book = ModelMapper(await bookRepository.FindBookById(apiRequestDTO.Id))
            };
        }

        public async Task<bool> UpdateBook(APIRequestModel apiRequestDTO)
        {
            return await bookRepository.UpdateBook(apiRequestDTO.Book, apiRequestDTO.Id);
        }

        public async Task<bool> DeleteBook(APIRequestModel apiRequestDTO)
        {
            return await bookRepository.DeleteBook(apiRequestDTO.Id);
        }

        public async Task<APIResponseModel> GetAllBook()
        {
            var books = await bookRepository.GetAllBooks();
            return new APIResponseModel
            {
                Books = books.Select(b => ModelMapper(b)).ToList()
            }; 
        }


        private Book EntityMapper(BookModel bookDTO)
        {
            return new Book
            {
                Name = bookDTO.Name,
                Description = bookDTO.Description,
            };
        }

        private BookModel ModelMapper(Book book)
        {
            return new BookModel
            {
                Description = book.Description,
                Name = book.Name,
            };
        }
    }
}
