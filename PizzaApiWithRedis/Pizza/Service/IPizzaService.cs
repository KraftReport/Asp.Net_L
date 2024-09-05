using PizzaApiWithRedis.Pizza.Model;

namespace PizzaApiWithRedis.Pizza.Service
{
    public interface IPizzaService
    {
        public Task<int> addPizzaToCatalog(ApiRequestDto pizzaRequest);
        public Task<List<string>> getListOfPizzaNames();
        public Task<ApiResponseDto> getPizzaById(int id); 
        public Task<byte[]> getPizzaPhoto(int id);
        public Task<bool> deletePizza(int id);
        public Task<ApiResponseDto> updatePizzaById(int id,ApiRequestDto updateRequest);
    }
}
