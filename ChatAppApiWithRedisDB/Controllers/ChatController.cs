using ChatAppApiWithRedisDB.Model; 
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace ChatAppApiWithRedisDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IConnectionMultiplexer _redis;

        public ChatController(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

/*        [HttpPost]
        public async Task<IActionResult> sendMessage([FromBody]ChatMessage chatMessage)
        {
            await chatService.sendMessage(chatMessage);
            return Ok(chatMessage);
        }*/

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] ChatMessage message)
        {
            await _redis.GetDatabase().ListRightPushAsync("messages", $"{message.Content}");
            var db = _redis.GetDatabase();
            db.Publish("chat_channel", message.Content);
            return Ok();
        }

        [HttpGet("subscribe")]
        public async Task Subscribe()
        {
            var response = Response;
            response.ContentType = "text/event-stream";
            var sub = _redis.GetSubscriber();

            await sub.SubscribeAsync("chat_channel", async (channel, message) =>
            {
                await response.WriteAsync($"data: {message}\n\n");
                await response.Body.FlushAsync();
            });

            while (!HttpContext.RequestAborted.IsCancellationRequested)
            {
                await Task.Delay(1000); // Keep the connection alive
            }
        }
    }
}
