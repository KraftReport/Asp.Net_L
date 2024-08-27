using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PlateDirectPaymentApi.DirectPaymentModule.Enum;

namespace PlateDirectPaymentApi.DirectPaymentModule.Entity
{
    [Table(name: "currencyTable")]
    public class PlateCurrency
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(24)")]
        public PlateType PlateType { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal PlateCount { get; set; }
        public int MemberId { get; set; }
    }
}
