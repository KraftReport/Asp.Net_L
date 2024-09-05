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

        [HttpGet("{id}")]
        public async Task<IActionResult> getPizzaDetail(int id)
        {
            return Ok(await pizzaService.getPizzaById(id)); 
        }

        [HttpGet("/photo/{id}")]
        public async Task<IActionResult> getPizzaPhoto(int id)
        {
            return File(await pizzaService.getPizzaPhoto(id), "image/jpeg");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> updatePizza(int id,[FromForm]ApiRequestDto apiRequestDto)
        {
            return Ok(await pizzaService.updatePizzaById(id,apiRequestDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deletePizza(int id)
        {
            return Ok(await pizzaService.deletePizza(id));
        }

        [HttpGet("/pizzas/list")]
        public async Task<IActionResult> allPizzaDetail()
        {
            return Ok(await pizzaService.GetAllPizzaDataAsync());
        }
    }
}
