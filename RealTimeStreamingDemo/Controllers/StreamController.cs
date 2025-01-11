using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealTimeStreamingDemo.Streaming;
using System.Threading.Tasks;

namespace RealTimeStreamingDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StreamController : ControllerBase
    {
        private readonly StreamingService streamingService;

        public StreamController(StreamingService streamingService)
        {
            this.streamingService = streamingService;
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Upload(/*[FromForm]string url,*/[FromForm]string title, [FromForm]IFormFile file)
        {
            return Ok(await streamingService.UploadVideo(/*url,*/title,file));
        }

        [HttpPost]
        [Route("otp")]
        public IActionResult Otp([FromForm]string videoId)
        {
            return Ok(streamingService.GetOtpAndPlaybackInfo(videoId));
        }
         
    }
}
