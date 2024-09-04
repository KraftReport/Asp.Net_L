using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaApiWithRedis.Pizza.Model
{
    public class PizzaDetail
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal price { get; set; } = decimal.Zero;
        public string photo { get; set; } = string.Empty;
    }
}
