namespace MatchThree.Domain.Interfaces.UserSettings;

public interface IUpdateUserSettingsService
{
    Task UpdateCultureAsync(long userId, string newCulture);

    Task TurnOnNotificationsAsync(long userId);
    
    Task TurnOffNotificationsAsync(long userId);
}