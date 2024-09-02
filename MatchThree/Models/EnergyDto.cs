namespace MatchThree.API.Models;

public class EnergyDto
{
    public long Id { get; init; }
    public int CurrentReserve { get; init; }
    public int MaxReserve { get; init; }
    public TimeSpan RecoveryTime { get; init; }
    public DateTime? NearbyEnergyRecoveryAt { get; init; }
}