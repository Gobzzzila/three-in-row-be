namespace MatchThree.API.Models;

public class EnergyDto
{
    public long Id { get; set; }
    public int CurrentReserve { get; set; }
    public int MaxReserve { get; set; }
    public TimeSpan RecoveryTime { get; set; }
    public DateTime? NearbyEnergyRecoveryAt { get; set; }
}