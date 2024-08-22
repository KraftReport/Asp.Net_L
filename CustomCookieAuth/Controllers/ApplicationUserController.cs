using CustomCookieAuth.Attributes;
using CustomCookieAuth.Entities;
using CustomCookieAuth.Services;
using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc;

namespace CustomCookieAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private readonly ApplicationUserService applicationUserService;
        public ApplicationUserController(ApplicationUserService _applicationUserService) 
        {
            applicationUserService = _applicationUserService;
        }

        [HttpPut]
        [Authorize(Policy = "admin")]
        public async Task<ActionResult> updateUser([FromBody] ApplicationUser applicationuser)
        {
            return Ok(await applicationUserService.UpdateUser(applicationuser));
        }

        [HttpDelete("{Id}")]
        [Authorize(Policy = "admin")]
        public async Task<ActionResult> deleteUser(int Id)
        {
            return Ok(await applicationUserService.DeleteUser(Id));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> findById(int id)
        {
            return Ok(await applicationUserService.FindById(id));
        }

        [HttpGet]
        [LogHtokeMal("get all user info")]
        public async Task<ActionResult> findAll()
        {
            return Ok(await applicationUserService.FindAll());
        }
    }
}
