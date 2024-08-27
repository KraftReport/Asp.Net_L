namespace PlateDirectPaymentApi.DirectPaymentModule.Model
{
    public class PaymentDTO
    {
        public decimal Mmk { get; set; }
        public decimal Plate { get; set; }
        public string PlateType { get; set; }
        public int MemberId { get; set; }
    }
}
