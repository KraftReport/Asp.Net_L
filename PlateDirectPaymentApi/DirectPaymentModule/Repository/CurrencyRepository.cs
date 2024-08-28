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

            MakeNewRecord = async (PlateCurrency plateCurrency,PlateType plateType) =>
            {
                await applicationDbContext.PlateCurrency.AddAsync(plateCurrency);
                await applicationDbContext.SaveChangesAsync();
                return await FindRecordByMemberIdAndPlateType(plateCurrency.MemberId,plateType);
            };

            UpdateOldRecord = async (PlateCurrency plateCurrency,PlateType plateType) =>
            {
                var oldRecord = await FindRecordByMemberIdAndPlateType(plateCurrency.MemberId,plateType);
                oldRecord.PlateCount = oldRecord.PlateCount + plateCurrency.PlateCount;
                await applicationDbContext.SaveChangesAsync();
                return await FindRecordByMemberIdAndPlateType(plateCurrency.MemberId,plateType);
            };

            FindRecordByMemberIdAndPlateType = async (int Id,PlateType platetye) =>
            {
                return await applicationDbContext.PlateCurrency.FirstOrDefaultAsync(pc => pc.MemberId == Id && pc.PlateType == platetye);
            };

            GetPlateRecords = async () =>
            {
                return await applicationDbContext.PlateCurrency.ToListAsync();
            };

        }


        public Func<PlateCurrency,PlateType, Task<PlateCurrency>> MakeNewRecord {get;}
        public Func<PlateCurrency,PlateType, Task<PlateCurrency>> UpdateOldRecord { get; }
        public Func<int,PlateType,Task<PlateCurrency>> FindRecordByMemberIdAndPlateType { get; }
        public Func<Task<List<PlateCurrency>>> GetPlateRecords { get; }
    }
}
