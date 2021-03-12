using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace Proxy
{
    public class RedisConnection : IRedisConnection
    {
        private readonly IConfiguration _configuration;
        private readonly IDatabase _database;

        public RedisConnection(IConfiguration configuration)
        {
            _configuration = configuration;

            if(_database == null)
                _database = GetDatabase();
        }

        public IDatabase GetDatabase()
        {
            string cacheConnection = _configuration["CacheConnection"].ToString();
            return ConnectionMultiplexer.Connect(cacheConnection).GetDatabase();
        }

        public void Set(string key, string value)
        {
            _database.StringSet(key, value);
        }

        public string Get(string key)
        {
            return _database.StringGet(key);
        }
    }
}
