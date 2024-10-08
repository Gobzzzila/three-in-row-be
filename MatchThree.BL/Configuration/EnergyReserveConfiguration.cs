using MatchThree.Domain.Configuration;
using MatchThree.Domain.Interfaces;
using MatchThree.Domain.Interfaces.Upgrades;
using MatchThree.Shared.Constants;
using MatchThree.Shared.Enums;

namespace MatchThree.BL.Configuration;

public static class EnergyReserveConfiguration
{
    private static readonly Dictionary<EnergyReserveLevels, EnergyReserveParameters> EnergyReserveParams;
    
    public static (EnergyReserveLevels ReserveLevel, int ReserveValue) GetStartValue()
    {
        return (EnergyReserveLevels.Level1, EnergyReserveParams[EnergyReserveLevels.Level1].MaxReserve);
    }
    
    public static int GetReserveMaxValue(EnergyReserveLevels energyReserveLevel)
    {
        return EnergyReserveParams[energyReserveLevel].MaxReserve;
    }
    
    public static EnergyReserveParameters GetParamsByLevel(EnergyReserveLevels energyReserveLevel)
    {
        return EnergyReserveParams[energyReserveLevel];
    }
    
    //ctor
    static EnergyReserveConfiguration()
    {
        EnergyReserveParams = new Dictionary<EnergyReserveLevels, EnergyReserveParameters>//TODO make foreach and attributes as in fieldconfiguration
        {
            {
                EnergyReserveLevels.Level1, new EnergyReserveParameters
                {
                    MaxReserve = EnergyConstants.Level1EnergyReserve,
                    NextLevel = EnergyReserveLevels.Level2,
                    NextLevelCost = EnergyConstants.Level2EnergyReserveCost,
                    UpgradeCondition = null
                }
            },
            {
                EnergyReserveLevels.Level2, new EnergyReserveParameters
                {
                    MaxReserve = EnergyConstants.Level2EnergyReserve,
                    NextLevel = EnergyReserveLevels.Level3,
                    NextLevelCost = EnergyConstants.Level3EnergyReserveCost,
                    UpgradeCondition = null
                }
            },
            {
                EnergyReserveLevels.Level3, new EnergyReserveParameters
                {
                    MaxReserve = EnergyConstants.Level3EnergyReserve,
                    NextLevel = EnergyReserveLevels.Level4,
                    NextLevelCost = EnergyConstants.Level4EnergyReserveCost,
                    UpgradeCondition = null
                }
            },
            {
                EnergyReserveLevels.Level4, new EnergyReserveParameters
                {
                    MaxReserve = EnergyConstants.Level4EnergyReserve,
                    NextLevel = EnergyReserveLevels.Level5,
                    NextLevelCost = EnergyConstants.Level5EnergyReserveCost,
                    UpgradeCondition = null
                }
            },
            {
                EnergyReserveLevels.Level5, new EnergyReserveParameters
                {
                    MaxReserve = EnergyConstants.Level5EnergyReserve,
                    NextLevel = EnergyReserveLevels.Level6,
                    NextLevelCost = EnergyConstants.Level6EnergyReserveCost,
                    UpgradeCondition = null
                }
            },
            {
                EnergyReserveLevels.Level6, new EnergyReserveParameters
                {
                    MaxReserve = EnergyConstants.Level6EnergyReserve,
                    NextLevel = EnergyReserveLevels.Level7,
                    NextLevelCost = EnergyConstants.Level7EnergyReserveCost,
                    UpgradeCondition = UpgradeCondition(EnergyConstants.Level7EnergyReserveReferralAmount)
                }
            },
            {
                EnergyReserveLevels.Level7, new EnergyReserveParameters
                {
                    MaxReserve = EnergyConstants.Level7EnergyReserve,
                    NextLevel = EnergyReserveLevels.Level8,
                    NextLevelCost = EnergyConstants.Level8EnergyReserveCost,
                    UpgradeCondition = null
                }
                
            },
            {
                EnergyReserveLevels.Level8, new EnergyReserveParameters
                {
                    MaxReserve = EnergyConstants.Level8EnergyReserve,
                    NextLevel = EnergyReserveLevels.Level9,
                    NextLevelCost = EnergyConstants.Level9EnergyReserveCost,
                    UpgradeCondition = null
                }
            },
            {
                EnergyReserveLevels.Level9, new EnergyReserveParameters
                {
                    MaxReserve = EnergyConstants.Level9EnergyReserve,
                    NextLevel = EnergyReserveLevels.Level10,
                    NextLevelCost = EnergyConstants.Level10EnergyReserveCost,
                    UpgradeCondition = UpgradeCondition(EnergyConstants.Level10EnergyReserveReferralAmount)
                }
            },
            {
                EnergyReserveLevels.Level10, new EnergyReserveParameters
                {
                    MaxReserve = EnergyConstants.Level10EnergyReserve,
                    NextLevel = EnergyReserveLevels.Level11,
                    NextLevelCost = EnergyConstants.Level11EnergyReserveCost,
                    UpgradeCondition = null
                }
            },
            {
                EnergyReserveLevels.Level11, new EnergyReserveParameters
                {
                    MaxReserve = EnergyConstants.Level11EnergyReserve,
                    NextLevel = EnergyReserveLevels.Level12,
                    NextLevelCost = EnergyConstants.Level12EnergyReserveCost,
                    UpgradeCondition = UpgradeCondition(EnergyConstants.Level12EnergyReserveReferralAmount)
                }
            },
            {
                EnergyReserveLevels.Level12, new EnergyReserveParameters
                {
                    MaxReserve = EnergyConstants.Level12EnergyReserve,
                    NextLevel = EnergyReserveLevels.Level13,
                    NextLevelCost = EnergyConstants.Level13EnergyReserveCost,
                    UpgradeCondition = null
                }
            },
            {
                EnergyReserveLevels.Level13, new EnergyReserveParameters
                {
                    MaxReserve = EnergyConstants.Level13EnergyReserve,
                    NextLevel = EnergyReserveLevels.Level14,
                    NextLevelCost = EnergyConstants.Level14EnergyReserveCost,
                    UpgradeCondition = UpgradeCondition(EnergyConstants.Level14EnergyReserveReferralAmount)
                }
            },
            {
                EnergyReserveLevels.Level14, new EnergyReserveParameters
                {
                    MaxReserve = EnergyConstants.Level14EnergyReserve,
                    NextLevel = EnergyReserveLevels.Level15,
                    NextLevelCost = EnergyConstants.Level15EnergyReserveCost,
                    UpgradeCondition = null
                }
            },
            {
                EnergyReserveLevels.Level15, new EnergyReserveParameters
                {
                    MaxReserve = EnergyConstants.Level15EnergyReserve,
                    NextLevel = null,
                    NextLevelCost = null,
                    UpgradeCondition = null
                }
            }
        };
    }

    private static Func<IUpgradesRestrictionsService, long, Task<bool>> UpgradeCondition(int requiredReferralsAmount)
    {
        return (upgradesRestrictionsService, userId) => 
            upgradesRestrictionsService.ValidateEnergyReserveRestrictions(userId, requiredReferralsAmount);
    }
}