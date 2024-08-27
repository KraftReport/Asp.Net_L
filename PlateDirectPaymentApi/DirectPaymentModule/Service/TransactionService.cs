using PlateDirectPaymentApi.DirectPaymentModule.Entity;
using PlateDirectPaymentApi.DirectPaymentModule.Repository;

namespace PlateDirectPaymentApi.DirectPaymentModule.Service
{
    public class TransactionService
    {
        private readonly TransactionRepository transactionRepository;
        public TransactionService (TransactionRepository transactionRepository)
        {
            this.transactionRepository = transactionRepository;

            MakeTransaction = async (Transaction transaction) =>
            {
                return await transactionRepository.MakeTransaction(transaction);
            };

            GetTransactionList = async () =>
            {
                return await transactionRepository.GetTransactionList();
            };
        }

        public Func<Transaction,Task<string>> MakeTransaction { get; }

        public Func<Task<List<Transaction>>> GetTransactionList { get; }
    }
}
