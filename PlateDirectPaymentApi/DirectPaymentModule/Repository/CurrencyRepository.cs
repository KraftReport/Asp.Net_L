using Microsoft.EntityFrameworkCore;
using PlateDirectPaymentApi.Database;
using PlateDirectPaymentApi.DirectPaymentModule.Entity;
using PlateDirectPaymentApi.DirectPaymentModule.Enum;
using PlateDirectPaymentApi.DirectPaymentModule.Model;

namespace PlateDirectPaymentApi.DirectPaymentModule.Repository
{
    public class CurrencyRepository
    {
        private readonly ApplicationDbContext applicationDbContext; 
        

        public CurrencyRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext; 
        }


        public async Task<PlateCurrency> MakeNewRecord(PlateCurrency plateCurrency,PlateType plateType)
        {
            await applicationDbContext.PlateCurrency.AddAsync(plateCurrency);
            await applicationDbContext.SaveChangesAsync();
            return await FindRecordByMemberIdAndPlateType(plateCurrency.MemberId, plateType);
        }

        public async Task<PlateCurrency> UpdateOldRecord(PlateCurrency plateCurrency,PlateType plateType)
        {
            var oldRecord = await FindRecordByMemberIdAndPlateType(plateCurrency.MemberId, plateType);
            oldRecord.PlateCount = oldRecord.PlateCount + plateCurrency.PlateCount;
            await applicationDbContext.SaveChangesAsync();
            return await FindRecordByMemberIdAndPlateType(plateCurrency.MemberId, plateType);
        }

        public async Task<PlateCurrency> FindRecordByMemberIdAndPlateType(int Id,PlateType plateType)
        {
            return await applicationDbContext.PlateCurrency.FirstOrDefaultAsync(pc => pc.MemberId == Id && pc.PlateType == plateType);
        }

        public async Task<List<PlateCurrency>> GetPlateRecords()
        {
            return await applicationDbContext.PlateCurrency.ToListAsync();
        }

        public async Task<bool> updateRecord(int id,PaymentDTO payment)
        {
            var record = await applicationDbContext.PlateCurrency.FindAsync(id);
            record.PlateCount = payment.Plate;
            return await applicationDbContext.SaveChangesAsync() > 0 ;

        }

        public async Task<bool> deleteRecord(int id)
        {
            var record = await applicationDbContext.PlateCurrency.FindAsync(id);
            record.IsDeleted = true;
            return await applicationDbContext.SaveChangesAsync() > 0;
        }

        public async Task<PlateCurrency> findById(int id)
        {
            return await applicationDbContext.PlateCurrency.FindAsync(id);
        }
    }
}
