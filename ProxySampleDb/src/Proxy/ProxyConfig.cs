using Microsoft.Extensions.Primitives;
using Microsoft.ReverseProxy.Abstractions;
using Microsoft.ReverseProxy.Service;
using System.Collections.Generic;
using System.Threading;

namespace Proxy
{
    public class ProxyConfig : IProxyConfig
    {
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        public ProxyConfig(IReadOnlyList<ProxyRoute> routes, IReadOnlyList<Cluster> clusters)
        {
            Routes = routes;
            Clusters = clusters;
            ChangeToken = new CancellationChangeToken(_cts.Token);
        }

        public IReadOnlyList<ProxyRoute> Routes { get; }

        public IReadOnlyList<Cluster> Clusters { get; }

        public IChangeToken ChangeToken { get; }
    }
}
