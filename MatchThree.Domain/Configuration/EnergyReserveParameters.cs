using MatchThree.Domain.Interfaces.Referral;
using MatchThree.Shared.Enums;

namespace MatchThree.Domain.Configuration;

public record EnergyReserveParameters
{
    public int MaxReserve { get; init; }
    public EnergyReserveLevels? NextLevel { get; init; }
    public uint? NextLevelCost { get; init; }
    public Func<IReadReferralService, long, Task<bool>>? UpgradeCondition { get; init; } 
}