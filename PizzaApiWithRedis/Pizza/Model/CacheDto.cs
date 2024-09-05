namespace PizzaApiWithRedis.Pizza.Model
{
    public class CacheDto
    {
        public int id {  get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string photo { get; set; }
        public decimal price { get; set; }
        public string base64String { get; set; }
    }
}
