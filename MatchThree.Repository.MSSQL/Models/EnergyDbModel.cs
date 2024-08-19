using System.ComponentModel.DataAnnotations.Schema;
using MatchThree.Repository.MSSQL.Models.Base;
using MatchThree.Shared.Enums;

namespace MatchThree.Repository.MSSQL.Models;

[Table("Energies")]
public class EnergyDbModel : DbModel
{
    public ushort CurrentReserve { get; set; }
    public EnergyReserveLevels MaxReserve { get; set; }
    public EnergyRecoveryLevels RecoveryLevel { get; set; }
    public ushort AvailableEnergyDrinkAmount { get; set; }
    public ushort PurchasableEnergyDrinkAmount { get; set; }
    public DateTime? LastRecoveryStartTime { get; set; }
    public UserDbModel? User { get; set; }
}