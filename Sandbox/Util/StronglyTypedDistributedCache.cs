using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace Sandbox.Util;

public class StronglyTypedDistributedCache : IStronglyTypedDistributedCache
{
    private readonly IDistributedCache _cache;

    public StronglyTypedDistributedCache(IDistributedCache cache)
    {
        _cache = cache;
    }
        
    public Task SetAsync<TValue>(string key, TValue value, DistributedCacheEntryOptions options)
    {
        return _cache.SetAsync(key, JsonSerializer.SerializeToUtf8Bytes(value), options);
    }

    public async Task<TValue?> GetAsync<TValue>(string key)
    {
        var cacheValue = await _cache.GetAsync(key);
        return cacheValue == null
            ? default
            : JsonSerializer.Deserialize<TValue>(Encoding.UTF8.GetString(cacheValue));
    }

    public Task InvalidateAsync(string key)
    {
        return _cache.RemoveAsync(key);
    }
}