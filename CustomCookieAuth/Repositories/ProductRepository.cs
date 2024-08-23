using CustomCookieAuth.Data;
using CustomCookieAuth.Entities;
using CustomCookieAuth.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomCookieAuth.Repositories
{
    public class ProductRepository
    {
        private readonly ApplicationDatabaseContext applicationDatabaseContext;

        public ProductRepository(ApplicationDatabaseContext _applicationDatabaseContext)
        {
            applicationDatabaseContext = _applicationDatabaseContext;

            CreateProduct = async (product) =>
            {
                await _applicationDatabaseContext.Products.AddAsync(product);
                await _applicationDatabaseContext.SaveChangesAsync();
                return product.Id;
            };

            FindProduct = async (id) =>
            {
                return await applicationDatabaseContext.Products.FindAsync(id);
            };

            GetProducts = async () =>
            {
                return await applicationDatabaseContext.Products.ToListAsync();
            };

            UpdateProduct = async (dto) =>
            {
                var found = await FindProduct(dto.Id);
                found.Name = dto.Name;
                found.Description = dto.Description;
                found.Price = dto.Price;
                await applicationDatabaseContext.SaveChangesAsync();
                return found;
            };

            DeleteProduct = async (id) =>
            {
                var found = await FindProduct(id);
                applicationDatabaseContext.Products.Remove(found);
                await applicationDatabaseContext.SaveChangesAsync();
                return true;
            };
        }

        public Func<Product, Task<int>> CreateProduct {get;}

        public Func<int,Task<Product>> FindProduct { get;}

        public Func<Task<List<Product>>> GetProducts { get;}

        public Func<ProductDTO,Task<Product>> UpdateProduct { get;}

        public Func<int,Task<bool>> DeleteProduct { get;}

 
         
    }
}
