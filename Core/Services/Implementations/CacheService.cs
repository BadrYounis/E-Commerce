using Domain.Contracts;
using Services.Abstraction.Contracts;

namespace Services.Implementations;
public class CacheService
    (ICacheRepository _cacheRepository) : ICacheService
{
    public async Task<string?> GetCachedValueAsync(string cachedKey)
        => await _cacheRepository.GetAsync(cachedKey);  
    public async Task SetCacheValueAsync(string key, object value, TimeSpan timeToLive)
        => await _cacheRepository.SetAsync(key, value, timeToLive);  
}