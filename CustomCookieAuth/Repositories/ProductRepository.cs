using CustomCookieAuth.Data;

namespace CustomCookieAuth.Repositories
{
    public class ProductRepository
    {
        private readonly ApplicationDatabaseContext applicationDatabaseContext;

        public ProductRepository(ApplicationDatabaseContext _applicationDatabaseContext)
        {
            applicationDatabaseContext = _applicationDatabaseContext;
        }

      //  public async Task<bool> CreateProduct(Product)
    }
}
