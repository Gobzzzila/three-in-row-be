using MatchThree.Domain.Models.Upgrades;

namespace MatchThree.Domain.Interfaces.Upgrades;

public interface IGetUpgradesService
{
    Task<IReadOnlyCollection<GroupedUpgradesEntity>> GetAll(long userId);
}