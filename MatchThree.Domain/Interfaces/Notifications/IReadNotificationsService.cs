using MatchThree.Domain.Models;

namespace MatchThree.Domain.Interfaces.Notifications;

public interface IReadNotificationsService
{
    Task<IReadOnlyCollection<NotificationsEntity>> GetTargetsOfEnergyNotificationAsync();
}