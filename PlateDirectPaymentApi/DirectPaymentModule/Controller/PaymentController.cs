using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlateDirectPaymentApi.DirectPaymentModule.Model;
using PlateDirectPaymentApi.DirectPaymentModule.Service;

namespace PlateDirectPaymentApi.DirectPaymentModule.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ICurrencyService currencyService;

        public PaymentController(ICurrencyService currencyService)
        {
            this.currencyService = currencyService;
        }

        [HttpPost]
        public async Task<IActionResult> MakePayment([FromBody]PaymentDTO paymentDTO)
        {
            return Ok(await currencyService.MakePayment(paymentDTO));
        }

        [HttpGet]
        public async Task<IActionResult> GetPlateRecord()
        {
            return Ok(await currencyService.GetPlateRecord());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> findById(int id)
        {
            return Ok(await currencyService.findById(id));
        }

        [HttpPut]
        public async Task<IActionResult> updateRecord(int id,[FromBody]PaymentDTO paymentDTO)
        {
            return Ok(await currencyService.updateRecord(id, paymentDTO));
        }

        [HttpDelete]
        public async Task<IActionResult> deleteRecord(int id)
        {
            return Ok(await currencyService.deleteRecord(id));
        }

    }
}
