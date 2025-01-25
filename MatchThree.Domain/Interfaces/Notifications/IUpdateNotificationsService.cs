using MatchThree.Domain.Models;

namespace MatchThree.Domain.Interfaces.Notifications;

public interface IUpdateNotificationsService
{
    ValueTask ResetEnergyNotificationTimeAsync(long userId);
    Task SetEnergyNotificationTimeAsync(long userId, DateTime? notificationTime);
    Task ActualizeEnergyNotificationTimeAsync(long userId);
}