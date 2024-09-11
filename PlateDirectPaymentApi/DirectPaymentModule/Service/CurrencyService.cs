using PlateDirectPaymentApi.DirectPaymentModule.Entity;
using PlateDirectPaymentApi.DirectPaymentModule.Enum;
using PlateDirectPaymentApi.DirectPaymentModule.Exception;
using PlateDirectPaymentApi.DirectPaymentModule.Model;
using PlateDirectPaymentApi.DirectPaymentModule.Repository;
using System.Diagnostics.Eventing.Reader;

namespace PlateDirectPaymentApi.DirectPaymentModule.Service
{
    public class CurrencyService : ICurrencyService
    {
        private readonly CurrencyRepository currencyRepository;
        private readonly TransactionRepository transactionRepository;

        public CurrencyService(CurrencyRepository currencyRepository,TransactionRepository transactionRepository)
        {
            this.transactionRepository = transactionRepository;
            this.currencyRepository = currencyRepository;
  
        }

        public  async Task<bool> MakePayment(PaymentDTO paymentDTO)
        {
            validatePaymentRequest(paymentDTO);
            var Type = GetPlateTypeFromString(paymentDTO.PlateType);

            if (await CheckOldRecord(paymentDTO))
            {
                var result1 = await currencyRepository.UpdateOldRecord(plateCurrencyMapper(paymentDTO), Type);
                Console.WriteLine(await MakeTransaction(result1));
                return result1 != null ? true : false;
            }
            var result2 = await currencyRepository.MakeNewRecord(plateCurrencyMapper(paymentDTO), Type);
            Console.WriteLine(await MakeTransaction(result2));
            return result2 != null ? true : false;
        }

        private PlateCurrency plateCurrencyMapper(PaymentDTO paymentDTO)
        {
            var typeOfPlate = paymentDTO.PlateType == "GOLD" ? PlateType.GOLD : PlateType.SILVER;

            return new PlateCurrency
            {
                PlateType = typeOfPlate,
                MemberId = paymentDTO.MemberId,
                PlateCount = paymentDTO.Plate,
            };
        }

        private PaymentDTO paymentDTOMapper(PlateCurrency plateCurrency)
        {
            return new PaymentDTO
            {
                MemberId = plateCurrency.MemberId,
                Plate = plateCurrency.PlateCount,
                PlateType = plateCurrency.PlateType.ToString()
            };
        }
        private async Task<bool> CheckOldRecord(PaymentDTO paymentDTO )
        {
            var Type = GetPlateTypeFromString(paymentDTO.PlateType);
            var member = await currencyRepository.FindRecordByMemberIdAndPlateType(paymentDTO.MemberId, Type);
            return (member != null && member.PlateType + "" == paymentDTO.PlateType) ? true : false;
        }


        private async Task<string> MakeTransaction(PlateCurrency plate)
        {
            var transaction = new Transaction
            {
                plateCount = plate.PlateCount + "",
                TranscationTime = DateTime.Now,
                UserId = plate.MemberId,
                plateType = plate.PlateType.ToString(),
            };
            return await transactionRepository.MakeTransaction(transaction);
        }
        public async Task<List<PaymentDTO>> GetPlateRecord()
        {
            var records = await currencyRepository.GetPlateRecords();
            var filtered = records.Where(r => !r.IsDeleted);
            return filtered.Select(paymentDTOMapper).ToList();
        }

        private PlateType GetPlateTypeFromString(string plateType)
        {
            return plateType == "GOLD" ? PlateType.GOLD : PlateType.SILVER;
        }

        public async Task<PaymentDTO?> findById(int id)
        {
            var record = await currencyRepository.findById(id);
            return record.IsDeleted ?  null : paymentDTOMapper(record);
        }

        public async Task<bool> updateRecord(int id,PaymentDTO paymentDTO)
        {
            return await currencyRepository.updateRecord(id, paymentDTO);
        }

        public async Task<bool> deleteRecord(int id)
        {
            return await currencyRepository.deleteRecord(id);
        }

        private PlateCurrency validatePaymentRequest(PaymentDTO paymentDTO)
        {
            if (string.IsNullOrWhiteSpace(paymentDTO.Mmk.ToString()))
            {
                throw new PaymentServiceRequestInvalidException("payment amount is required");
            }

            if (string.IsNullOrWhiteSpace(paymentDTO.Plate.ToString()))
            {
                throw new PaymentServiceRequestInvalidException("amount of plate to fill is required");
            }

            if(string.IsNullOrWhiteSpace(paymentDTO.PlateType.ToString()))
            {
                throw new PaymentServiceRequestInvalidException("plate type to fill is required");
            }

            if(string.IsNullOrWhiteSpace(paymentDTO.MemberId.ToString()))
            {
                throw new PaymentServiceRequestInvalidException("id of the member is required to fill the plate");
            }

     /*       if(paymentDTO.PlateType != "GOLD" ||  paymentDTO.PlateType != "SILVER")
            {
                throw new PaymentServiceRequestInvalidException("request plate type format must be 'GOLD' or 'SILVER' only");
            }*/

            return plateCurrencyMapper(paymentDTO);
        }

    }
}
