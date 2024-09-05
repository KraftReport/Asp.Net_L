namespace PizzaApiWithRedis.Pizza.Service
{
    public interface ICacheManagerService
    {
        public Task<bool> saveIntoCacheDbWithKey<T>(string key,T value);
        public Task<T> findDataInTheCache<T>(string key);
        public Task<bool> deleteFromCache(string key);
        public Task<bool> saveIntoHash(string key, string value, string index);
        public Task<List<T>> findHashListInTheCache<T>(string key);
        public Task<bool> findDataInCacheHash(string key, string index);
    }
}
