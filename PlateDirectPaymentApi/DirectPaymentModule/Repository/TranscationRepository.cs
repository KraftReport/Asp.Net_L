using Microsoft.EntityFrameworkCore;
using PlateDirectPaymentApi.Database;
using PlateDirectPaymentApi.DirectPaymentModule.Entity; 

namespace PlateDirectPaymentApi.DirectPaymentModule.Repository
{
    public class TransactionRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public TransactionRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;

            MakeTransaction = async (Transaction transaction) =>
            {
                await applicationDbContext.Transactions.AddAsync(transaction);
                await applicationDbContext.SaveChangesAsync();
                return "time : " + transaction.TranscationTime + " | member_id : " + transaction.UserId + " | updated_plateCount :"+ transaction.plateCount + " | plateType : "+transaction.plateType;
            };

            GetTransactionList = async () =>
            {
                return await applicationDbContext.Transactions.ToListAsync();
            };
        }

        public Func<Transaction,Task<string>> MakeTransaction { get; }

        public Func<Task<List<Transaction>>> GetTransactionList { get; }
    }
}
