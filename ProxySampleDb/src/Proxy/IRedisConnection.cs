using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proxy
{
    public interface IRedisConnection
    {
        void Set(string key, string value);

        string Get(string key);
    }
}
