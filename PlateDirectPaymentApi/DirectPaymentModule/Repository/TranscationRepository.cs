using Microsoft.EntityFrameworkCore; 
using PlateDirectPaymentApi.Database;
using PlateDirectPaymentApi.DirectPaymentModule.Entity;
using PlateDirectPaymentApi.DirectPaymentModule.Helper;

namespace PlateDirectPaymentApi.DirectPaymentModule.Repository
{
    public class TransactionRepository
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly LogHelper logHelper;
        public TransactionRepository(ApplicationDbContext applicationDbContext, LogHelper logHelper)
        {
            this.logHelper = logHelper;
            this.applicationDbContext = applicationDbContext;
             
        }

        public async Task<string> MakeTransaction(Transaction transaction)
        {
            await applicationDbContext.Transactions.AddAsync(transaction);
            await applicationDbContext.SaveChangesAsync();
            var info = "time : " + transaction.TranscationTime + " | member_id : " + transaction.UserId + " | updated_plateCount :" + transaction.plateCount + " | plateType : " + transaction.plateType;
            logHelper.Info(info);
            return info;
        }

        public async Task<List<Transaction>> GetTransactionList()
        {
            return await applicationDbContext.Transactions.ToListAsync();
        }
    }
}
