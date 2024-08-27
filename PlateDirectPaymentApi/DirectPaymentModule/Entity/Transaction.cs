using System.ComponentModel.DataAnnotations;

namespace PlateDirectPaymentApi.DirectPaymentModule.Entity
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        public DateTime TranscationTime { get; set; } = DateTime.Now;
        public int UserId { get; set; }
        public string plateCount { get; set; }
        public string plateType { get; set; }
    }
}
