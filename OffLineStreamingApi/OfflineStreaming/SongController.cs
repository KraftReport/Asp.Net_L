using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OffLineStreamingApi.OfflineStreaming
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private readonly SongService _songService;

        public SongController(SongService songService)
        {
            _songService = songService;
        }

        [HttpPost]
        public async Task<IActionResult> SaveSong([FromQuery]string name)
        {
            return Ok(await _songService.SaveSong(name)); 
        }
    }
}
