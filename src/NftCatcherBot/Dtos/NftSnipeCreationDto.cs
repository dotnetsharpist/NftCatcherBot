using NftCatcherApi.Entities;
using NftCatcherApi.Enums;

namespace NftCatcherApi.Dtos;

public class NftSnipeCreationDto
{
    public string GiftId { get; set; } = null!; // e.g., "JesterHat"
    public int TargetNumber { get; set; }         // e.g., 777
    public SnipeStatus Status { get; set; } // "Pending", "Completed", "Failed"
    public long BotUserId { get; set; }
    public BotUser BotUser { get; set; }
}