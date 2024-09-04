/*using ChatAppApiWithRedisDB.Model;
using Microsoft.AspNetCore.SignalR;
using StackExchange.Redis;

namespace ChatAppApiWithRedisDB.Service
{
    public class RedisSubscriberService : BackgroundService
    {
        private readonly IConnectionMultiplexer connection;
        private readonly IHubContext<ChatHub> context;

        public RedisSubscriberService(IConnectionMultiplexer subscriber, IHubContext<ChatHub> context)
        {
            this.connection = subscriber;
            this.context = context;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await connection.GetSubscriber().SubscribeAsync("chat_channel", async (channel, message) =>
            {
                await context.Clients.All.SendAsync("ReceiveMessage", "RedisSubscriber", message);
                
            });
            await Task.CompletedTask;
        }
    }
}
*/