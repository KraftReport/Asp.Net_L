using CustomCookieAuth.Attributes;

namespace CustomCookieAuth.Entities
{
    public class Product
    {
        public int Id { get; set; }
        [LogHtokeMal("this is product name")]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
