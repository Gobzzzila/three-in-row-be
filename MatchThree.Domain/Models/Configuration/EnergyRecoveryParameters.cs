using MatchThree.Shared.Enums;

namespace MatchThree.Domain.Models.Configuration;

public record EnergyRecoveryParameters
{
    public TimeSpan RecoveryTime { get; init; }
    public EnergyRecoveryLevels? NextLevel { get; init; }
}