using System.Collections.Frozen;
using MatchThree.Domain.Configuration;
using MatchThree.Domain.Interfaces.Upgrades;
using MatchThree.Shared.Constants;
using MatchThree.Shared.Enums;

namespace MatchThree.BL.Configuration;

public static class EnergyRecoveryConfiguration
{
    private static readonly FrozenDictionary<EnergyRecoveryLevels, EnergyRecoveryParameters> EnergyRecoveryParams;
    
    public static EnergyRecoveryLevels GetStartValue()
    {
        return EnergyRecoveryLevels.Level1;
    }
    
    public static TimeSpan GetRecoveryTime(EnergyRecoveryLevels energyRecoveryLevel)
    {
        return EnergyRecoveryParams[energyRecoveryLevel].RecoveryTime;
    }
    
    public static TimeSpan GetNextLevelRecoveryTime(EnergyRecoveryLevels energyRecoveryLevel)
    {
        var levelParams = GetParamsByLevel(energyRecoveryLevel);
        return levelParams.NextLevel is not null
            ? EnergyRecoveryParams[levelParams.NextLevel.Value].RecoveryTime
            : default;
    }
    
    public static EnergyRecoveryParameters GetParamsByLevel(EnergyRecoveryLevels energyRecoveryLevel)
    {
        return EnergyRecoveryParams[energyRecoveryLevel];
    }
    
    static EnergyRecoveryConfiguration()
    {
        var dictionary = new Dictionary<EnergyRecoveryLevels, EnergyRecoveryParameters>//TODO make foreach and attributes as in fieldconfiguration
        {
            {
                EnergyRecoveryLevels.Level1, new EnergyRecoveryParameters
                {
                    RecoveryTime = EnergyConstants.Level1EnergyRecovery,
                    NextLevel = EnergyRecoveryLevels.Level2,
                    NextLevelCost = EnergyConstants.Level2EnergyRecoveryCost,
                    UpgradeCondition = UpgradeCondition(EnergyReserveLevels.Level2)
                }
            },
            {
                EnergyRecoveryLevels.Level2, new EnergyRecoveryParameters
                {
                    RecoveryTime = EnergyConstants.Level2EnergyRecovery,
                    NextLevel = EnergyRecoveryLevels.Level3,
                    NextLevelCost = EnergyConstants.Level3EnergyRecoveryCost,
                    UpgradeCondition = UpgradeCondition(EnergyReserveLevels.Level4)
                }
            },
            {
                EnergyRecoveryLevels.Level3, new EnergyRecoveryParameters
                {
                    RecoveryTime = EnergyConstants.Level3EnergyRecovery,
                    NextLevel = EnergyRecoveryLevels.Level4,
                    NextLevelCost = EnergyConstants.Level4EnergyRecoveryCost,
                    UpgradeCondition = UpgradeCondition(EnergyReserveLevels.Level7)
                }
            },
            {
                EnergyRecoveryLevels.Level4, new EnergyRecoveryParameters
                {
                    RecoveryTime = EnergyConstants.Level4EnergyRecovery,
                    NextLevel = EnergyRecoveryLevels.Level5,
                    NextLevelCost = EnergyConstants.Level5EnergyRecoveryCost,
                    UpgradeCondition = UpgradeCondition(EnergyReserveLevels.Level11)
                }
            },
            {
                EnergyRecoveryLevels.Level5, new EnergyRecoveryParameters
                {
                    RecoveryTime = EnergyConstants.Level5EnergyRecovery,
                    NextLevel = EnergyRecoveryLevels.Level6,
                    NextLevelCost = EnergyConstants.Level6EnergyRecoveryCost,
                    UpgradeCondition = UpgradeCondition(EnergyReserveLevels.Level15)
                }
            },
            {
                EnergyRecoveryLevels.Level6, new EnergyRecoveryParameters
                {
                    RecoveryTime = EnergyConstants.Level6EnergyRecovery,
                    NextLevel = null,
                    NextLevelCost = null,
                    UpgradeCondition = null
                }
            },
        };

        EnergyRecoveryParams = dictionary.ToFrozenDictionary();
    }
    
    private static Func<IUpgradesRestrictionsService, EnergyReserveLevels, int?> UpgradeCondition(EnergyReserveLevels restrictedLevel)
    {
        return (upgradesRestrictionsService, currentLevel) => 
            upgradesRestrictionsService.ValidateEnergyReserveLevel(currentLevel, restrictedLevel);
    }
}