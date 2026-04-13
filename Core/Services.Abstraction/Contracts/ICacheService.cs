namespace Services.Abstraction.Contracts;
public interface ICacheService
{
    //Get
    Task<string?> GetCachedValueAsync(string cachedKey);

    //Set
    Task SetCacheValueAsync(string key, object value, TimeSpan timeToLive);
}
