using Microsoft.EntityFrameworkCore;
using ProjectFrameCRUD.Data;
using ProjectFrameCRUD.DTO;
using ProjectFrameCRUD.Models;

namespace ProjectFrameCRUD.Repository
{
    public class BookRepository
    {
        private readonly AppDbContext appDbContext;
        private readonly DbSet<Book> book;
        public BookRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
            book = this.appDbContext.Books;
        }

        public async Task<bool> SaveBook(Book book)
        {
            await this.book.AddAsync(book);
            return await appDbContext.SaveChangesAsync() > 0;
        }

        public async Task<Book> FindBookById(int id)
        {
            return await this.book.FindAsync(id); 
        }

        public async Task<bool> UpdateBook(BookDTO book,int id)
        {
            var found = await FindBookById(id);
            found.Description = book.Description;
            found.Name = book.Name;
            return await appDbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteBook(int id)
        {
            var found = await FindBookById(id);
            this.book.Remove(found);
            return await appDbContext.SaveChangesAsync() > 0;
        }

        public async Task<List<Book>> GetAllBooks()
        {
            return await book.ToListAsync();
        }
    }
}
