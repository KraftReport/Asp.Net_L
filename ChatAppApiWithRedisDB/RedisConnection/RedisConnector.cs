using StackExchange.Redis;

namespace ChatAppApiWithRedisDB.RedisConnection
{
    public class RedisConnector
    {
        private readonly IConnectionMultiplexer connection;
        public RedisConnector(IConnectionMultiplexer connection)
        {
            this.connection = connection;
        }

        public IDatabase getDatabase()
        {
            return connection.GetDatabase();
        }

        public ISubscriber getSubscriber()
        {
            return connection.GetSubscriber();  
        }
    }
}
