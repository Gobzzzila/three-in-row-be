using MatchThree.Domain.Interfaces.Upgrades;
using MatchThree.Shared.Enums;

namespace MatchThree.Domain.Configuration;

public record EnergyReserveParameters
{
    public int MaxReserve { get; init; }
    public EnergyReserveLevels? NextLevel { get; init; }
    public uint? NextLevelCost { get; init; }
    public Func<IUpgradesRestrictionsService, long, Task<int?>>? UpgradeCondition { get; init; } 
}