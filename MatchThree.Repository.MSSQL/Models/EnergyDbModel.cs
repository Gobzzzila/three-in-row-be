﻿using System.ComponentModel.DataAnnotations.Schema;
using MatchThree.Repository.MSSQL.Models.Base;
using MatchThree.Shared.Constants;
using MatchThree.Shared.Enums;

namespace MatchThree.Repository.MSSQL.Models;

[Table(EnergyConstants.EnergyTableName)]
public class EnergyDbModel : DbModel
{
    public int CurrentReserve { get; set; }
    public EnergyReserveLevels MaxReserve { get; set; }
    public EnergyRecoveryLevels RecoveryLevel { get; set; }
    public int AvailableEnergyDrinkAmount { get; set; }
    public int PurchasableEnergyDrinkAmount { get; set; }
    public int UsedEnergyDrinkCounter { get; set; }
    public int PurchasedEnergyDrinkCounter { get; set; }
    public DateTime? LastRecoveryStartTime { get; set; }
    public UserDbModel? User { get; set; }
}