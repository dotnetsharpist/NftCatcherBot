using StackExchange.Redis;

namespace NftCatcherApi.Infrastructure;

public class RedisConfig(string connectionString)
{
    private readonly ConnectionMultiplexer _redis = ConnectionMultiplexer.Connect(connectionString);

    public IDatabase GetDatabase() => _redis.GetDatabase();
}
