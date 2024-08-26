using MatchThree.Shared.Enums;

namespace MatchThree.Domain.Configuration;

public record EnergyRecoveryParameters
{
    public TimeSpan RecoveryTime { get; init; }
    public EnergyRecoveryLevels? NextLevel { get; init; }
    public uint? NextLevelCost { get; init; }
}