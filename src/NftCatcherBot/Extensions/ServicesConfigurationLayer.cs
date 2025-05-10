using NftCatcherApi.Handlers;
using NftCatcherApi.Infrastructure;
using NftCatcherApi.Mappers;
using NftCatcherApi.Services;

namespace NftCatcherApi.Extensions;

public static class ServicesConfigurationLayer
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<BotUserService>();
        builder.Services.AddScoped<UpdateHandler>();
        builder.Services.ConfigureTelegramBotMvc();
        builder.Services.AddScoped<StateService>();
        builder.Services.AddScoped<RedisService>();
        builder.Services.AddAutoMapper(typeof(MapperProfile));
    }
}