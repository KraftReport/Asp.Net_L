using PlateDirectPaymentApi.DirectPaymentModule.Model;

namespace PlateDirectPaymentApi.DirectPaymentModule.Service
{
    public interface ICurrencyService
    {
        public Task<bool> MakePayment(PaymentDTO paymentDTO);
        public Task<List<PaymentDTO>> GetPlateRecord();
    }
}
