using MatchThree.Shared.Enums;

namespace MatchThree.Domain.Models.Configuration;

public record EnergyReserveParameters
{
    public int MaxValue { get; init; }
    public EnergyReserveLevels? NextLevel { get; init; }
}