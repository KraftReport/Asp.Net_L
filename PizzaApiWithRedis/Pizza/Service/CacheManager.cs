using Newtonsoft.Json;
using StackExchange.Redis;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PizzaApiWithRedis.Pizza.Service
{
    public class CacheManager : ICacheManagerService
    {
        private readonly IDatabase database;

        public CacheManager()
        {
            var redis = ConnectionMultiplexer.Connect("localhost:6379");
            this.database = redis.GetDatabase();
        }

        public async Task<T> findDataInTheCache<T>(string key)
        {
            var found = await database.StringGetAsync(key);
            if (found.IsNull)
            {
                return default;
            }
            return JsonConvert.DeserializeObject<T>(found);
        }

        public async Task<bool> saveIntoCacheDbWithKey<T>(string key,T value)
        {
            return await database.StringSetAsync(key,JsonConvert.SerializeObject(value));
        }

        public async Task<bool> deleteFromCache(string key)
        {
            return await database.KeyDeleteAsync(key);
        }
    }
}
