namespace MatchThree.Domain.Interfaces.UserSettings;

public interface IDeleteUserSettingsService
{
    Task DeleteAsync(long id);
}