/*using ChatAppApiWithRedisDB.Model;
using Microsoft.AspNetCore.SignalR;
using StackExchange.Redis;

namespace ChatAppApiWithRedisDB.Service
{
    public class ChatService
    {
        private readonly IHubContext<ChatHub> hubContext; 
        private readonly IConnectionMultiplexer connection;
        
        public ChatService(IHubContext<ChatHub> hub, IConnectionMultiplexer subscriber)
        {
            this.hubContext = hub;
            this.connection = subscriber; 
        }

        public async Task sendMessage(ChatMessage message)
        {
            await connection.GetDatabase().ListRightPushAsync("chat_message", $"{message.user} : {message.message}");
            await connection.GetSubscriber().PublishAsync("chat_channel", $"{message.user} : {message.message}");
            await hubContext.Clients.All.SendAsync("ReceiveMessage", message.user, message.message);
        }
    }
}
*/