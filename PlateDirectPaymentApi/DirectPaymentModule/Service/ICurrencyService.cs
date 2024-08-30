using PlateDirectPaymentApi.DirectPaymentModule.Model;
using PlateDirectPaymentApi.DirectPaymentModule.Repository;

namespace PlateDirectPaymentApi.DirectPaymentModule.Service
{
    public interface ICurrencyService
    {
        public Task<bool> MakePayment(PaymentDTO paymentDTO);
        public Task<List<PaymentDTO>> GetPlateRecord();
        public Task<PaymentDTO> findById(int id);
        public Task<bool> updateRecord(int id, PaymentDTO paymentDTO);
        public Task<bool> deleteRecord(int id);
    }
}
