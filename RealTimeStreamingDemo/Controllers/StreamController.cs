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

        [HttpGet]
        [Route("audio-stream")]
        public async Task<IActionResult> AudioStream()
        {
            var audioFilePath = "C:/Users/KraftWork/Desktop/BaDin/snnncnmt.mp3";
            var icecastUrl = "http://localhost:8000/stream.mp3";
            var sourcePassword = "hackme";

            try
            {
                using (var httpClient = new HttpClient())
                { 
                    var authHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes($"source:{sourcePassword}"));
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeader);
                     
                    using (var fileStream = new FileStream(audioFilePath, FileMode.Open, FileAccess.Read))
                    {
                        var content = new StreamContent(fileStream);
                        content.Headers.ContentType = new MediaTypeHeaderValue("audio/mpeg");
                         
                        var response = await httpClient.PostAsync(icecastUrl, content);

                        if (response.IsSuccessStatusCode)
                        {
                            return Ok("Streaming started successfully.");
                        }
                        else
                        {
                            return BadRequest($"Failed to start streaming. Status code: {response.StatusCode}. Message: {await response.Content.ReadAsStringAsync()}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }


    }
}
