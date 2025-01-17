using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealTimeStreamingDemo.Streaming;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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

        [HttpPost]
        [Route("split")]
        public IActionResult Split([FromForm]string filePath)
        {
            try
            {
                streamingService.GenerateStreamingFiles(filePath);
                return Ok("ok"); 
            }
            catch(Exception e)
            {
                throw new Exception("error creating streaming files");
            } 
        } 

        [HttpPost]
        [Route("change-key-location")]
        public IActionResult ChangeKeyLocation([FromForm]string m3u8FilePath, [FromForm]string newKeyFileLocation)
        {
            streamingService.EditKeyFileLocation(m3u8FilePath, newKeyFileLocation);
            return Ok("ok");
        }


    }
}
