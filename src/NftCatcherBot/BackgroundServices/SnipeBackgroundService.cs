using System.Net;
using Telegram.Bot;

namespace NftCatcherApi.BackgroundServices;

public class SnipeBackgroundService : BackgroundService
{
    private readonly ILogger<SnipeBackgroundService> _logger;
    private readonly HttpClient _httpClient;
    // Sample: set your target manually
    private readonly string giftType = "BdayCandle";
    private readonly int targetNumber = 125560;
    private ITelegramBotClient bot;

    public SnipeBackgroundService(ILogger<SnipeBackgroundService> logger, ITelegramBotClient bot)
    {
        _logger = logger;
        var handler = new HttpClientHandler
        {
            AllowAutoRedirect = false
        };

        _httpClient = new HttpClient(handler);
        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (SniperBot)");
        this.bot = bot;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var checkNumber = targetNumber - 1;

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var url = $"https://t.me/nft/{giftType}-{checkNumber}";
                var response = await _httpClient.GetAsync(url, stoppingToken);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var text = $"GIFT FOUND !!!\n\n{url}";
                    await bot.SendMessage(chatId: 8124349344, text, cancellationToken: stoppingToken);
                    // TODO: Trigger gift upgrade here

                    break; // stop checking after success
                }
                else
                {
                    var text = $"{giftType}-{checkNumber} not live yet. Status: {response.StatusCode}";
                    await bot.SendMessage(chatId: 8124349344, text, cancellationToken: stoppingToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error checking gift status");
            }
            finally
            {
                await Task.Delay(3000, stoppingToken); // 3s between checks
            }
        }
    }
}