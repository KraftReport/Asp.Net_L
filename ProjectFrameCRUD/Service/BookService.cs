using ProjectFrameCRUD.DTO;
using ProjectFrameCRUD.Models;
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

        public async Task<bool> RegisterBook(APIRequestDTO apiRequestDTO)
        {
            return await bookRepository.SaveBook(EntityMapper(apiRequestDTO.Book));
        }

        public async Task<APIResponseDTO> FindBookById(APIRequestDTO apiRequestDTO)
        {
            return new APIResponseDTO
            {
                Book = DTOMapper(await bookRepository.FindBookById(apiRequestDTO.Id))
            };
        }

        public async Task<bool> UpdateBook(APIRequestDTO apiRequestDTO)
        {
            return await bookRepository.UpdateBook(apiRequestDTO.Book, apiRequestDTO.Id);
        }

        public async Task<bool> DeleteBook(APIRequestDTO apiRequestDTO)
        {
            return await bookRepository.DeleteBook(apiRequestDTO.Id);
        }

        public async Task<APIResponseDTO> GetAllBook()
        {
            var books = await bookRepository.GetAllBooks();
            return new APIResponseDTO
            {
                Books = books.Select(b => DTOMapper(b)).ToList()
            }; 
        }


        private Book EntityMapper(BookDTO bookDTO)
        {
            return new Book
            {
                Name = bookDTO.Name,
                Description = bookDTO.Description,
            };
        }

        private BookDTO DTOMapper(Book book)
        {
            return new BookDTO
            {
                Description = book.Description,
                Name = book.Name,
            };
        }
    }
}
