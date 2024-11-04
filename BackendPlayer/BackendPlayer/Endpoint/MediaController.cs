using LibVLCSharp.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BackendPlayer.Endpoint
{
    [ApiController]
    [Route("api/[controller]")]
    public class MediaController : ControllerBase 
    {
        private readonly LibVLC _libVLC;
        private readonly MediaPlayer _mediaPlayer;
        private bool _disposed = false;

        public MediaController()
        {
            Core.Initialize();
             
            string libvlcDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "VLC");
            if (!Directory.Exists(libvlcDirectory))
            {
                throw new DirectoryNotFoundException($"VLC directory not found at {libvlcDirectory}");
            }
             
            _libVLC = new LibVLC(new[] { "--plugin-path=" + Path.Combine(libvlcDirectory, "plugins") });
            _mediaPlayer = new MediaPlayer(_libVLC);
        }

        [HttpPost("play")]
        public IActionResult PlayMedia([FromQuery] string mediaUrl)
        {
            if (string.IsNullOrEmpty(mediaUrl))
            {
                return BadRequest("Media URL is required.");
            }

            try
            {
                var media = new Media(_libVLC, new Uri(mediaUrl));
                _mediaPlayer.Media = media;
            

                _mediaPlayer.Play();
                return Ok("Media playback started.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error starting media playback: {ex.Message}");
            }
        }

        [HttpPost("pause")]
        public IActionResult PauseMedia()
        {
            if (_mediaPlayer.IsPlaying)
            {
                _mediaPlayer.Pause();
                return Ok("Media playback paused.");
            }
            return BadRequest("Media is not currently playing.");
        }

        [HttpPost("stop")]
        public IActionResult StopMedia()
        {
            if (_mediaPlayer.IsPlaying)
            {
                _mediaPlayer.Stop();
                return Ok("Media playback stopped.");
            }
            return BadRequest("Media is not currently playing.");
        }

        [HttpGet("status")]
        public IActionResult GetStatus()
        {
            if (_mediaPlayer.IsPlaying)
            {
                return Ok("Media is currently playing.");
            }
            return Ok("Media is not playing.");
        }

 
 
    }
}
