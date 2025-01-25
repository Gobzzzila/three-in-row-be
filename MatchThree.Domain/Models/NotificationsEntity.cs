using MatchThree.Shared.Enums;

namespace MatchThree.Domain.Models;

public class NotificationsEntity
{
    public long Id { get; set; }
    public DateTime? EnergyNotification { get; set; }
    public CultureTypes Culture { get; set; }
}