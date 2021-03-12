using Microsoft.ReverseProxy.Abstractions;
using Microsoft.ReverseProxy.Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Proxy
{
    public class ProxyConfigProvider : IProxyConfigProvider
    {
        private readonly IRedisConnection _connection;

        public ProxyConfigProvider(IRedisConnection connection)
        {
            _connection = connection;
        }

        public IProxyConfig GetConfig()
        {
            return LoadProxyConfig();
        }

        private ProxyConfig LoadProxyConfig()
        {
            var proxyData = _connection.Get("ReverseProxy");

            JObject jsonData = JObject.Parse(proxyData);

            // getting routes
            var routesData = jsonData.SelectToken("$.Routes").ToString();
            var routes = JsonConvert.DeserializeObject<List<ProxyRoute>>(routesData);

            // getting cluster
            var clusterData = jsonData["Clusters"].Select(x => x).ToList();

            var clusters = new List<Cluster>();

            foreach (var item in clusterData)
            {
                var destinations = new Dictionary<string, Destination>();

                foreach (var dst in ((JProperty)item).Value.SelectTokens("Destinations").AsEnumerable())
                {
                    destinations = destinations.Concat(dst.ToObject<Dictionary<string, Destination>>())
                                               .ToDictionary(x => x.Key, x => x.Value);
                }

                Cluster clusterItem = new Cluster()
                {
                    Id = ((JProperty)item).Name,
                    Destinations = destinations
                };

                clusters.Add(clusterItem);
            }

            return new ProxyConfig(routes, clusters);
        }
    }

}
