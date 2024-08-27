using PlateDirectPaymentApi.DirectPaymentModule.Entity;
using PlateDirectPaymentApi.DirectPaymentModule.Enum;
using PlateDirectPaymentApi.DirectPaymentModule.Model;
using PlateDirectPaymentApi.DirectPaymentModule.Repository;

namespace PlateDirectPaymentApi.DirectPaymentModule.Service
{
    public class CurrencyService
    {
        private readonly CurrencyRepository currencyRepository;
        private readonly TransactionRepository transactionRepository;

        public CurrencyService(CurrencyRepository currencyRepository,TransactionRepository transactionRepository)
        {
            this.transactionRepository = transactionRepository;
            this.currencyRepository = currencyRepository;


            MakePayment = async (PaymentDTO paymentDTO) =>
            {
                var Type = GetPlateTypeFromString(paymentDTO.PlateType);

                if (await CheckOldRecord(paymentDTO))
                {
                     var result1 = await currencyRepository.UpdateOldRecord(plateCurrencyMapper(paymentDTO),Type);
                    Console.WriteLine(await MakeTransaction(result1));
                    return result1 != null ? true : false;
                }
                var result2 = await currencyRepository.MakeNewRecord(plateCurrencyMapper(paymentDTO),Type);
                Console.WriteLine(await MakeTransaction(result2));
                return result2 != null ? true : false;
            };


            CheckOldRecord = async (PaymentDTO paymentDTO) =>
            {
                var Type = GetPlateTypeFromString(paymentDTO.PlateType);
                var member = await currencyRepository.FindRecordByMemberId(paymentDTO.MemberId,Type);
                return (member != null && member.PlateType +"" == paymentDTO.PlateType) ? true : false;
            };


            MakeTransaction = async (PlateCurrency plate) =>
            {
                var transaction = new Transaction
                {
                    plateCount = plate.PlateCount +"",
                    TranscationTime = DateTime.Now,
                    UserId = plate.MemberId,
                    plateType = plate.PlateType.ToString(),
                };
                return await transactionRepository.MakeTransaction(transaction);
            };


            plateCurrencyMapper = (PaymentDTO paymentDTO) =>
            {
                var typeOfPlate = paymentDTO.PlateType == "GOLD" ? PlateType.GOLD : PlateType.SILVER;

                return new PlateCurrency
                {
                    PlateType = typeOfPlate,
                    MemberId = paymentDTO.MemberId,
                    PlateCount = paymentDTO.Plate,
                };
               
            };


            GetPlateRecord = async () =>
            {
                return await currencyRepository.GetPlateRecords();
            };


            GetPlateTypeFromString = (string plateType) =>
            {
                return plateType == "GOLD" ? PlateType.GOLD : PlateType.SILVER;
            };

        }

        public Func<PaymentDTO, Task<bool>> MakePayment { get; }

        private Func<PaymentDTO, PlateCurrency> plateCurrencyMapper { get; }

        private Func<PaymentDTO, Task<bool>> CheckOldRecord {get ;}

        private Func<PlateCurrency,Task<string>> MakeTransaction { get; }
         
        public Func<Task<List<PlateCurrency>>> GetPlateRecord { get; }

        private Func<string,PlateType> GetPlateTypeFromString { get; }

    }
}
