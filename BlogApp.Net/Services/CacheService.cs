using StackExchange.Redis;
using System.Text.Json;

namespace BlogApp.Net.Services
{
    public class CacheService:ICacheService
    {
        private IDatabase _cachedb
        public CacheService()
        {
            var redis = ConnectionMultiplexer.Connect("localhost:6379");
            _cachedb = redis.GetDatabase();
        }

        public T GetData<T>(string key)
        {
            var value = _cachedb.StringGet(key);
            if(!string.IsNullOrEmpty(value))
            {
                return JsonSerializer.Deserialize<T>(value);
            }
            return default;
        }

        public object RemoveData(string key)
        {
            var exists = _cachedb.KeyExists(key);

            if (exists)
            {
               return _cachedb.KeyDelete(key);
            }
            return false;
        }

        public bool setData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            var expiryTime = expirationTime.DateTime.AddMinutes(2);
            var isSet = _cachedb.StringSet(key,JsonSerializer.Serialize(value),expiryTime);
            return isSet;
        }
    }
}
