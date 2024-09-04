namespace PizzaApiWithRedis.Pizza.Model
{
    public class ApiRequestDto
    {
        public string name { get; set; }
        public string description { get; set; } 
        public decimal price { get; set; }
        public IFormFile photo { get; set; }
    }
}
