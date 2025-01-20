using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealTimeStreamingDemo.Streaming;
using System; 
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

        [HttpPost]
        [Route("change-ts-location")]   
        public IActionResult ChangeTsLocation([FromForm]string m3u8FilePath, [FromForm]string newTsFileLocation)
        {
            streamingService.EditTsFileLocation(m3u8FilePath, newTsFileLocation);
            return Ok("ok");
        }

        [HttpGet]
        [Route("get-otk")]
        public IActionResult GetOtk(string token)
        {
            try
            {
                var fileBytes = System.IO.File.ReadAllBytes($"C:\\Users\\KraftWork\\Desktop\\wow\\{token}\\encryption.key");
                //System.IO.File.Delete($"http://localhost:9002/{token}/encryption.key");
                System.IO.File.Delete("C:\\Users\\KraftWork\\Desktop\\wow\\12345\\encryption.key");
                return File(fileBytes, "application/octet-stream", "encryption.key");
            }
            catch
            {
                return NotFound("token is invalid");
            }
        }

        [HttpGet]
        [Route("hello")]
        public IActionResult Hello()
        {
            return Ok("hello");
        }


    }
}
