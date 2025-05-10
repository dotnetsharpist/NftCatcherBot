using System.Text.Json;
using StackExchange.Redis;

namespace NftCatcherApi.Infrastructure;

public class RedisService(RedisConfig redisConfig)
{
    private readonly IDatabase _db = redisConfig.GetDatabase();

    public async Task SetValueAsync(string key, object value)
    {
        var json = JsonSerializer.Serialize(value);
        await _db.StringSetAsync(key, json);
    }

    public async Task<T?> GetValueAsync<T>(string key)
    {
        var value = await _db.StringGetAsync(key);
        return value.IsNullOrEmpty ? default : JsonSerializer.Deserialize<T>(value!);
    }

    public async Task DeleteValueAsync(string key)
    {
        await _db.KeyDeleteAsync(key);
    }
}
