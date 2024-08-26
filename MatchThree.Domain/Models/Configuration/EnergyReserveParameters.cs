using MatchThree.Shared.Enums;

namespace MatchThree.Domain.Models.Configuration;

public record EnergyReserveParameters
{
    public int MaxReserve { get; init; }
    public EnergyReserveLevels? NextLevel { get; init; }
    public uint? NextLevelCost { get; init; }
}