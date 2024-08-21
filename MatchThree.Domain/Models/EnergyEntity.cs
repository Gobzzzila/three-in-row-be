namespace MatchThree.Domain.Models;

public class EnergyEntity
{
    public long Id { get; set; }
    public int CurrentReserve { get; set; }
    public int MaxReserve { get; set; }
    public TimeSpan RecoveryTime { get; set; }
    public DateTime? NearbyEnergyRecoveryAt { get; set; }
}