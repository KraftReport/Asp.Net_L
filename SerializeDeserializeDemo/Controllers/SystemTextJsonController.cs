using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SerializeDeserializeDemo.Helper;
using SerializeDeserializeDemo.Models;
using Serilog;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace SerializeDeserializeDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemTextJsonController : ControllerBase
    {
        private readonly LogService logService;
        public SystemTextJsonController(LogService logService)
        {
            this.logService = logService;
        }

        [HttpPost]
        public IActionResult GetRequestData([FromBody] TestRequestDTO testRequestDTO)
        {
            var list = new TestRequestDTO().ModalList;
            var jsonString =   JsonSerializer.Serialize(testRequestDTO);
            logService.Info("json string => " +jsonString);
            var obj = JsonSerializer.Deserialize<TestRequestDTO>(jsonString);
            var objList = obj.ModalList;
            var objListList = obj.ModalListList;
            var objDictionary = obj.ModalDictionary;
            var objDictionaryDictionary = obj.ModalDictionaryDictionary;
            logService.Info("\n \n json obj list => " +JsonSerializer.Serialize(objList));
            logService.Info("\n \n json obj list list => " + JsonSerializer.Serialize(objListList));
            logService.Info("\n \n json obj dictionary => " + JsonSerializer.Serialize(objDictionary));
            logService.Info("\n \n json obj dictioarny dictionary => " + JsonSerializer.Serialize(objDictionaryDictionary));
            return Ok(jsonString);
        }
    }
}
