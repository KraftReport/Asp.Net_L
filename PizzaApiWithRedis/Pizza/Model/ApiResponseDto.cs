namespace PizzaApiWithRedis.Pizza.Model
{
    public class ApiResponseDto
    {
        public PizzaDetail pizzaObject {  get; set; } 
        public string Base64String { get; set; }
    }
}
