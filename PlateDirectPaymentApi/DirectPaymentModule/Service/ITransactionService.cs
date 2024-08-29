

using PlateDirectPaymentApi.DirectPaymentModule.Entity;

namespace PlateDirectPaymentApi.DirectPaymentModule.Service
{
    public interface ITransactionService
    {
        public Task<List<Transaction>> GetTransactionList();
        public Task<string> MakeTransaction(Transaction transaction);
    }
}
