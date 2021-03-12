using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace Proxy
{
    public static class Bootstrap
    {
        public static void CacheInitialize(IServiceCollection services)
        {
            var item = File.ReadAllText(@"data.json");

            var redisConnection = services.BuildServiceProvider().GetService<IRedisConnection>();
            redisConnection.Set("ReverseProxy", item);
        }
    }
}
