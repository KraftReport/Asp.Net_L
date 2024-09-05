using Newtonsoft.Json;
using StackExchange.Redis; 

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

        public async Task<List<T>> findHashListInTheCache<T>(string key)
        {
            var found = await database.HashGetAllAsync(key); 
            if(found == null)
            {
                return null;
            }
            var jsonString = JsonConvert.SerializeObject(found.ToDictionary(
                f => f.Name.ToString(),
                f => f.Value.ToString()
                ));
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string,string>>(jsonString);
            return dictionary.Values.Select(v => JsonConvert.DeserializeObject<T>(v)).ToList();
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

        public async Task<bool> saveIntoHash(string key,string value,string index)
        {
            return await database.HashSetAsync(key,index,value);
        }

        public async Task<bool> findDataInCacheHash(string key,string index)
        {
            return await database.HashGetAsync(key,index) !=  default(RedisValue);
        }
 
 
    }
}
