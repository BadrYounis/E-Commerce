using Domain.Contracts;
using StackExchange.Redis;
using System.Text.Json;

namespace Persistence.Repositories;
public class CacheRepository(IConnectionMultiplexer _connection) : ICacheRepository
{
    private readonly IDatabase _database = _connection.GetDatabase();
    public async Task<string?> GetAsync(string cachedKey)
    {
        var value = await _database.StringGetAsync(cachedKey);
        return value.IsNullOrEmpty ? default : value;
    }
    public async Task SetAsync(string key, object value, TimeSpan timeToLive)
    {
        var serializedObject = JsonSerializer.Serialize(value);
        await _database.StringSetAsync(key, serializedObject, timeToLive);
    }
}