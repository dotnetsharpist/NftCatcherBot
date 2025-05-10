using NftCatcherApi.Enums;

namespace NftCatcherApi.Entities;

public class NftSnipe : Auditable
{
    public string GiftType { get; set; } = null!; // e.g., "JesterHat"
    public int TargetNumber { get; set; }         // e.g., 777
    public SnipeStatus Status { get; set; } // "Pending", "Completed", "Failed"
}