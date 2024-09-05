namespace PizzaApiWithRedis.Pizza.Service
{
    public interface ICacheManagerService
    {
        public Task<bool> saveIntoCacheDbWithKey<T>(string key,T value);
        public Task<T> findDataInTheCache<T>(string key);
        public Task<bool> deleteFromCache(string key);
    }
}
