using PlateDirectPaymentApi.DirectPaymentModule.Entity;
using PlateDirectPaymentApi.DirectPaymentModule.Repository;

namespace PlateDirectPaymentApi.DirectPaymentModule.Service
{
    public class TransactionService : ITransactionService
    {
        private readonly TransactionRepository transactionRepository;
        public TransactionService (TransactionRepository transactionRepository)
        {
            this.transactionRepository = transactionRepository;
             
         
        }

        public async Task<string> MakeTransaction(Transaction transaction)
        {
            return await transactionRepository.MakeTransaction(transaction);
        }

        public async Task<List<Transaction>> GetTransactionList()
        {
            return await transactionRepository.GetTransactionList();
        }
    }
}
