using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaApiWithRedis.Pizza.Model;
using PizzaApiWithRedis.Pizza.Service;

namespace PizzaApiWithRedis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly IPizzaService pizzaService;

        public PizzaController(IPizzaService pizzaService)
        {
            this.pizzaService = pizzaService;
        }

        [HttpPost]
        public async Task<IActionResult> addPizzaToCatalog([FromForm]ApiRequestDto apiRequestDto)
        {
            return Ok(await pizzaService.addPizzaToCatalog(apiRequestDto));
        }

        [HttpGet]
        public async Task<IActionResult> getPizzaNameList()
        {
            return Ok(await pizzaService.getListOfPizzaNames());
        }
    }
}
