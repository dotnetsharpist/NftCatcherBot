using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NftCatcherApi.Handlers;
using NftCatcherApi.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace NftCatcherApi.Controllers;

[ApiController]
[Route("[controller]")]
public class BotController(IOptions<BotConfiguration> config) : ControllerBase
{
    [HttpGet("setWebhook")]
    public async Task<string> SetWebHook([FromServices] ITelegramBotClient bot, CancellationToken ct)
    {
        var webhookUrl = config.Value.BotWebhookUrl.AbsoluteUri;
        await bot.SetWebhook(webhookUrl, allowedUpdates: [], secretToken: config.Value.SecretToken, dropPendingUpdates: true, cancellationToken: ct);
        return $"Webhook set to {webhookUrl}";
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Update update, [FromServices] ITelegramBotClient bot, [FromServices] UpdateHandler handleUpdateService, CancellationToken ct)
    {
        if (Request.Headers["X-Telegram-Bot-Api-Secret-Token"] != config.Value.SecretToken)
            return Forbid();
        
        Console.WriteLine($"BusinessConnectionId: {update.BusinessMessage?.BusinessConnectionId ?? "null"}\nFrom: {update.BusinessMessage?.From?.FirstName ?? "null"}\nText: {update.BusinessMessage?.Text ?? "null"}\nMessageId: {update.BusinessMessage?.MessageId}");
        /*try
        {
            await handleUpdateService.HandleUpdateAsync(bot, update, ct);
        }
        catch (Exception exception)
        {
            await handleUpdateService.HandleErrorAsync(bot, exception,
                Telegram.Bot.Polling.HandleErrorSource.HandleUpdateError, ct);
        }*/
        return Ok();
    }
}