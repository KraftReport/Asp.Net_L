using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc; 

namespace TableProjectComponentServiceTestWebAPI.Audio
{
    [Route("api/[controller]")]
    [ApiController]
    public class AudioController : ControllerBase
    {
        private readonly AudioService audioService; 

        public AudioController(AudioService audioService)
        {
            this.audioService = audioService;
        }

        [HttpPost]
        public async Task<IActionResult> uploadAudio([FromForm] IFormFile file)
        {
            var id = await audioService.uploadAudio(file);
            return Ok(new { Id = id });
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> ListenMusic(int Id)
        {
            var audioDetails = await audioService.GetAudioDetailsAsync(Id);

            if (audioDetails == null)
            {
                return NotFound("Audio file not found.");
            }

            var mimeType = audioDetails[0]; // MIME type
            var filePath = audioDetails[2]; // File path
            var fileName = audioDetails[1];

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("File not found.");
            }

            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            // Add custom headers to discourage downloading
            Response.Headers.Add("X-Content-Type-Options", "nosniff");
            Response.Headers.Add("X-Frame-Options", "DENY");

            return new FileStreamResult(fileStream, mimeType)
            {
                FileDownloadName = null, // Do not suggest a file download
                EnableRangeProcessing = true, // Allow streaming/range requests
            };

        }
    }
}
