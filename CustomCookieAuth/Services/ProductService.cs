using CustomCookieAuth.Entities;
using CustomCookieAuth.Models;
using CustomCookieAuth.Repositories;

namespace CustomCookieAuth.Services
{
    public class ProductService
    {
        private readonly ProductRepository productRepository;
        public ProductService(ProductRepository _productRepository)
        {
            this.productRepository = _productRepository;
        }

        public async Task<int> CreateProduct(ProductDTO productDTO) =>
            await productRepository.CreateProduct(await MapProduct(productDTO));


        public async Task<Product> FindProduct(int Id)=>
            await productRepository.FindProduct(Id);

        public async Task<List<Product>> GetProducts() =>
            await productRepository.GetProducts();
        

        public async Task<Product> UpdateProduct(ProductDTO productDTO) =>
            await productRepository.UpdateProduct(productDTO);
        

        public async Task<bool> DeleteProduct(int Id)=>
            await productRepository.DeleteProduct(Id);
        

        private static Task<Product> MapProduct(ProductDTO productDTO) =>
            Task.FromResult(new Product
            {
                Id = productDTO.Id,
                Name = productDTO.Name,
                Description = productDTO.Description,
                Price = productDTO.Price
            });

    }
}
