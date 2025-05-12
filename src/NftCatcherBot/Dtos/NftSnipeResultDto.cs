using NftCatcherApi.Entities;
using NftCatcherApi.Enums;

namespace NftCatcherApi.Dtos;

public class NftSnipeResultDto
{
    public long Id { get; set; }
    public string GiftId { get; set; } = null!; // e.g., "JesterHat"
    public int TargetNumber { get; set; }         // e.g., 777
    public SnipeStatus Status { get; set; } // "Pending", "Completed", "Failed"
    public long BotUserId { get; set; }
    public BotUser BotUser { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

}