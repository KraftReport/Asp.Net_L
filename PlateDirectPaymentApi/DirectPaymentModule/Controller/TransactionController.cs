using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlateDirectPaymentApi.DirectPaymentModule.Service;

namespace PlateDirectPaymentApi.DirectPaymentModule.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService transactionService;
        public TransactionController(ITransactionService transactionService)
        {
            this.transactionService = transactionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactions()
        {
            return Ok(await transactionService.GetTransactionList());
        }
    }
}
