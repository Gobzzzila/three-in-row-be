using MatchThree.Shared.Attributes;
using MatchThree.Shared.Constants;

namespace MatchThree.Shared.Enums;

public enum EnergyReserveLevels
{
    Undefined = 0,
    
    [UpgradeCost(EnergyConstants.Level2EnergyReserveCost)]
    Level1 = 1,
    
    [UpgradeCost(EnergyConstants.Level3EnergyReserveCost)]
    Level2 = 2,

    [UpgradeConditionArgument<int>(EnergyConstants.Level4EnergyReserveReferralAmount)]
    [UpgradeCost(EnergyConstants.Level4EnergyReserveCost)]
    Level3 = 3,
    
    [UpgradeCost(EnergyConstants.Level5EnergyReserveCost)]
    Level4 = 4,

    [UpgradeCost(EnergyConstants.Level6EnergyReserveCost)]
    Level5 = 5,

    [UpgradeConditionArgument<int>(EnergyConstants.Level7EnergyReserveReferralAmount)]
    [UpgradeCost(EnergyConstants.Level7EnergyReserveCost)]
    Level6 = 6,

    [UpgradeCost(EnergyConstants.Level8EnergyReserveCost)]
    Level7 = 7,

    [UpgradeCost(EnergyConstants.Level9EnergyReserveCost)]
    Level8 = 8,

    [UpgradeConditionArgument<int>(EnergyConstants.Level10EnergyReserveReferralAmount)]
    [UpgradeCost(EnergyConstants.Level10EnergyReserveCost)]
    Level9 = 9,

    [UpgradeCost(EnergyConstants.Level11EnergyReserveCost)]
    Level10 = 10,

    [UpgradeConditionArgument<int>(EnergyConstants.Level12EnergyReserveReferralAmount)]
    [UpgradeCost(EnergyConstants.Level12EnergyReserveCost)]
    Level11 = 11,

    [UpgradeCost(EnergyConstants.Level13EnergyReserveCost)]
    Level12 = 12,

    [UpgradeConditionArgument<int>(EnergyConstants.Level14EnergyReserveReferralAmount)]
    [UpgradeCost(EnergyConstants.Level14EnergyReserveCost)]
    Level13 = 13,

    [UpgradeCost(EnergyConstants.Level15EnergyReserveCost)]
    Level14 = 14,

    Level15 = 15,
}