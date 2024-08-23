using CustomCookieAuth.Entities;

namespace CustomCookieAuth.Models
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        public ProductDTO() { }

        public ProductDTO(Product product) =>
            (Id, Name, Price, Description) = (product.Id,product.Name,product.Price,product.Description);
    }
}
