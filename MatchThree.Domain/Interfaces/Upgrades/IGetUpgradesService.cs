using MatchThree.Domain.Models;

namespace MatchThree.Domain.Interfaces.Upgrades;

public interface IGetUpgradesService
{
    Task<IReadOnlyCollection<UpgradeEntity>> GetAll(long userId);
}