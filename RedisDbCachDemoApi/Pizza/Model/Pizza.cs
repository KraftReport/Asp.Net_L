using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RedisDbCachDemoApi.Pizza.Model
{
    [Table(name:"PizzaCatalog")]
    public class Pizza
    {
        [Key]
        public int id { get; set; } 
        public string name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public string photo { get; set; }
    }
}
