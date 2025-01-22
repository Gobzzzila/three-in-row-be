namespace MatchThree.Domain.Interfaces.UserSettings;

public interface IUpdateUserSettingsService
{
    Task UpdateCultureAsync(long id, string newCulture);

    Task TurnOnNotificationsAsync(long id);
    
    Task TurnOffNotificationsAsync(long id);
}