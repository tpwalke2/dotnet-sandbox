using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace Sandbox.Util;

public interface IStronglyTypedDistributedCache
{
    Task SetAsync<TValue>(
        string key,
        TValue value,
        DistributedCacheEntryOptions options);

    Task<TValue?> GetAsync<TValue>(string key);

    Task InvalidateAsync(string key);
}