using System.ComponentModel.DataAnnotations.Schema;
using MatchThree.Repository.MSSQL.Models.Base;
using MatchThree.Shared.Enums;

namespace MatchThree.Repository.MSSQL.Models;

[Table("Energies")]
public class EnergyDbModel : DbModel
{
    public int CurrentReserve { get; set; }
    public EnergyReserveLevels MaxReserve { get; set; }
    public EnergyRecoveryLevels RecoveryLevel { get; set; }
    public int AvailableEnergyDrinkAmount { get; set; }
    public int PurchasableEnergyDrinkAmount { get; set; }
    public DateTime? LastRecoveryStartTime { get; set; }
    public UserDbModel? User { get; set; }
}