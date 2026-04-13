namespace Domain.Contracts;
public interface ICacheRepository
{
    //Get: If Already Cached (Return Data) => Returns Cached Response
    Task<string?> GetAsync(string cachedKey);

    //Set: If No Cacing Happens (First Time To Call EndPoint) => Apply Caching And Put Data Into Redis
    Task SetAsync(string key, object value, TimeSpan timeToLive);
}