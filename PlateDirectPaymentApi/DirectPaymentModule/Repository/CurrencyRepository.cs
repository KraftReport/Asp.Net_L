using Microsoft.EntityFrameworkCore;
using PlateDirectPaymentApi.Database;
using PlateDirectPaymentApi.DirectPaymentModule.Entity;
using PlateDirectPaymentApi.DirectPaymentModule.Enum; 

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
    }
}
