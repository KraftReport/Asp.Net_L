using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SerializeDeserializeDemo.Helper;
using SerializeDeserializeDemo.Models;

namespace SerializeDeserializeDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewtonSoftController : ControllerBase
    {
        private readonly LogService logService;
        public NewtonSoftController(LogService logService)
        {
            this.logService = logService;
        }

        [HttpPost]
        public IActionResult TestJosnoPolicy([FromBody] TestModalDTO testModalDTO)
        {
            var jsonString = JsonConvert.SerializeObject(testModalDTO);
            logService.Info("\n \n json string => "+jsonString);
            var obj = JsonConvert.DeserializeObject<TestModalDTO>(jsonString);  
            return Ok(obj);
        }
    }
}
