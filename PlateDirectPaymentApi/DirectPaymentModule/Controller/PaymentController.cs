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
    }
}
