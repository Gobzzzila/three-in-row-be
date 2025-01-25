namespace MatchThree.Domain.Interfaces.Notifications;

public interface IDeleteNotificationsService
{
    Task DeleteAsync(long id);
}