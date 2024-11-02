using BackendPlayer.BackendPlayer.Implementation;
using BackendPlayer.BackendPlayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendPlayer.BackendPlayer.Endpoint
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayController : ControllerBase
    {
        private IMusicPlayer player;

        public PlayController(IMusicPlayer nAudioService)
        {
            player = nAudioService;
        }
         
        [HttpPost]
        public async Task<IActionResult> PlaySong([FromForm]string filepath)
        {  
            player.Load(filepath);
            player.Play();
            return Ok("song is playing");
        }

        [HttpPost("/pause")]
        public async Task<IActionResult> PauseSong()
        {
            player.Pause();
            return Ok("song is paused");
        }

        [HttpPost("/stop")]
        public async Task<IActionResult> StopSong()
        {
            player.Stop();
            return Ok("song is stopped");
        }
    }
}
