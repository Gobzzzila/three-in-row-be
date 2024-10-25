namespace MatchThree.API.Models;

public class EnergyDto
{
    public long Id { get; init; }
    public int CurrentReserve { get; init; }
    public int MaxReserve { get; init; }
    public int RecoveryTimeInSeconds { get; init; }
    public DateTimeOffset? NearbyEnergyRecoveryAt { get; init; }
}