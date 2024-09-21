using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PizzaApiWithRedis.Security.Service;

namespace PizzaApiWithRedis.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IJwtService jwtService) : ControllerBase
{
    [HttpPost]
    public IActionResult createToken()
    { 
        return Ok(jwtService.generateJwtToken("kraftreport04@gmail.com", "USR001"));
    }
}