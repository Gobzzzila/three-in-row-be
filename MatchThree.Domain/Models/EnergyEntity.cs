using MatchThree.Shared.Enums;

namespace MatchThree.Domain.Models;

public class EnergyEntity
{
    public long Id { get; set; }
    public int CurrentReserve { get; set; }
    public EnergyReserveLevels MaxReserve { get; set; }
    public EnergyRecoveryLevels RecoveryLevel { get; set; }
    public int AvailableEnergyDrinkAmount { get; set; }
    public int PurchasableEnergyDrinkAmount { get; set; }
    public DateTime? LastRecoveryStartTime { get; set; }
    public int PurchasedEnergyDrinkCounter { get; init; }
}