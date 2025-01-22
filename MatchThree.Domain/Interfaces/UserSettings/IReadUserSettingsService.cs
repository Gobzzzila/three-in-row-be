using MatchThree.Domain.Models;

namespace MatchThree.Domain.Interfaces.UserSettings;

public interface IReadUserSettingsService
{
    Task<UserSettingsEntity> GetByUserIdAsync(long userId);
}