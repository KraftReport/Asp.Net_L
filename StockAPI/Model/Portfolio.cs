using System.ComponentModel.DataAnnotations.Schema;

namespace StockAPI.Model
{
    [Table("portfolio")]
    public class Portfolio
    {
        public int StockId { get; set; }
        public string AppUserId { get; set; }
        public Stock Stock { get; set; }
        public AppUser appUser { get; set; }
    }
}
