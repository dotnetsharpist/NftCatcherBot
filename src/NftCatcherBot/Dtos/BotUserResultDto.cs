using NftCatcherApi.Enums;

namespace NftCatcherApi.Dtos;

public class BotUserResultDto
{
    public long UserId { get; set; }
    public string? Username { get; set; } 
    public string FullName { get; set; } = null!;
    public BotUserRole Role { get; set; } = BotUserRole.User;
}