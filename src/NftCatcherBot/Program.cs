using Microsoft.EntityFrameworkCore;
using NftCatcherApi.DbContexts;
using NftCatcherApi.Extensions;
using NftCatcherApi.Infrastructure;
using NftCatcherApi.Models;
using NftCatcherApi.Repositories;
using Telegram.Bot;

var builder = WebApplication.CreateBuilder(args);

// Setup Bot configuration
var botConfigSection = builder.Configuration.GetSection("BotConfiguration");
builder.Services.Configure<BotConfiguration>(botConfigSection);

// Register named HttpClient to get benefits of IHttpClientFactory
// and consume it with ITelegramBotClient typed client.
// More read:
//  https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests#typed-clients
//  https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests
builder.Services.AddHttpClient("tgwebhook").RemoveAllLoggers().AddTypedClient<ITelegramBotClient>(
    httpClient => new TelegramBotClient(botConfigSection.Get<BotConfiguration>()!.BotToken, httpClient));

var redisConnectionString = builder.Configuration.GetConnectionString("Redis");

// Register Redis as a singleton service
if (redisConnectionString != null) builder.Services.AddSingleton(new RedisConfig(redisConnectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<BotDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("local")));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.ConfigureServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
