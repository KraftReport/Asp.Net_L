using PizzaApiWithRedis.Pizza.Model;

namespace PizzaApiWithRedis.Pizza.Service
{
    public interface IPizzaService
    {
        public Task<int> addPizzaToCatalog(ApiRequestDto pizzaRequest);
        public Task<List<string>> getListOfPizzaNames();
    }
}
