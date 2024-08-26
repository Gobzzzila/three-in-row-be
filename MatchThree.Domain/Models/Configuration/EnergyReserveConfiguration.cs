using MatchThree.Shared.Constants;
using MatchThree.Shared.Enums;

namespace MatchThree.Domain.Models.Configuration;

public static class EnergyReserveConfiguration
{
    private static readonly Dictionary<EnergyReserveLevels, EnergyReserveParameters> EnergyReservesParams;
    
    public static (EnergyReserveLevels ReserveLevel, int ReserveValue) GetStartValue()
    {
        return (EnergyReserveLevels.Level1, EnergyReservesParams[EnergyReserveLevels.Level1].MaxReserve);
    }
    
    public static int GetReserveMaxValue(EnergyReserveLevels energyReserveLevel)
    {
        return EnergyReservesParams[energyReserveLevel].MaxReserve;
    }
    
    public static EnergyReserveParameters GetParamsByLevel(EnergyReserveLevels league)
    {
        return EnergyReservesParams[league];
    }
    
    //ctor
    static EnergyReserveConfiguration()
    {
        EnergyReservesParams = new Dictionary<EnergyReserveLevels, EnergyReserveParameters>
        {
            {
                EnergyReserveLevels.Level1, new EnergyReserveParameters
                {
                    MaxReserve = EnergyConstants.Level1EnergyReserve,
                    NextLevel = EnergyReserveLevels.Level2,
                    NextLevelCost = EnergyConstants.Level2EnergyReserveCost
                }
            },
            {
                EnergyReserveLevels.Level2, new EnergyReserveParameters
                {
                    MaxReserve = EnergyConstants.Level2EnergyReserve,
                    NextLevel = EnergyReserveLevels.Level3,
                    NextLevelCost = EnergyConstants.Level3EnergyReserveCost
                }
            },
            {
                EnergyReserveLevels.Level3, new EnergyReserveParameters
                {
                    MaxReserve = EnergyConstants.Level3EnergyReserve,
                    NextLevel = EnergyReserveLevels.Level4,
                    NextLevelCost = EnergyConstants.Level4EnergyReserveCost
                }
            },
            {
                EnergyReserveLevels.Level4, new EnergyReserveParameters
                {
                    MaxReserve = EnergyConstants.Level4EnergyReserve,
                    NextLevel = EnergyReserveLevels.Level5,
                    NextLevelCost = EnergyConstants.Level5EnergyReserveCost
                }
            },
            {
                EnergyReserveLevels.Level5, new EnergyReserveParameters
                {
                    MaxReserve = EnergyConstants.Level5EnergyReserve,
                    NextLevel = EnergyReserveLevels.Level6,
                    NextLevelCost = EnergyConstants.Level6EnergyReserveCost
                }
            },
            {
                EnergyReserveLevels.Level6, new EnergyReserveParameters
                {
                    MaxReserve = EnergyConstants.Level6EnergyReserve,
                    NextLevel = EnergyReserveLevels.Level7,
                    NextLevelCost = EnergyConstants.Level7EnergyReserveCost
                }
            },
            {
                EnergyReserveLevels.Level7, new EnergyReserveParameters
                {
                    MaxReserve = EnergyConstants.Level7EnergyReserve,
                    NextLevel = EnergyReserveLevels.Level8,
                    NextLevelCost = EnergyConstants.Level8EnergyReserveCost
                }
                
            },
            {
                EnergyReserveLevels.Level8, new EnergyReserveParameters
                {
                    MaxReserve = EnergyConstants.Level8EnergyReserve,
                    NextLevel = EnergyReserveLevels.Level9,
                    NextLevelCost = EnergyConstants.Level9EnergyReserveCost
                }
            },
            {
                EnergyReserveLevels.Level9, new EnergyReserveParameters
                {
                    MaxReserve = EnergyConstants.Level9EnergyReserve,
                    NextLevel = EnergyReserveLevels.Level10,
                    NextLevelCost = EnergyConstants.Level10EnergyReserveCost
                }
            },
            {
                EnergyReserveLevels.Level10, new EnergyReserveParameters
                {
                    MaxReserve = EnergyConstants.Level10EnergyReserve,
                    NextLevel = EnergyReserveLevels.Level11,
                    NextLevelCost = EnergyConstants.Level11EnergyReserveCost
                }
            },
            {
                EnergyReserveLevels.Level11, new EnergyReserveParameters
                {
                    MaxReserve = EnergyConstants.Level11EnergyReserve,
                    NextLevel = EnergyReserveLevels.Level12,
                    NextLevelCost = EnergyConstants.Level12EnergyReserveCost
                }
            },
            {
                EnergyReserveLevels.Level12, new EnergyReserveParameters
                {
                    MaxReserve = EnergyConstants.Level12EnergyReserve,
                    NextLevel = EnergyReserveLevels.Level13,
                    NextLevelCost = EnergyConstants.Level13EnergyReserveCost
                }
            },
            {
                EnergyReserveLevels.Level13, new EnergyReserveParameters
                {
                    MaxReserve = EnergyConstants.Level13EnergyReserve,
                    NextLevel = EnergyReserveLevels.Level14,
                    NextLevelCost = EnergyConstants.Level14EnergyReserveCost
                }
            },
            {
                EnergyReserveLevels.Level14, new EnergyReserveParameters
                {
                    MaxReserve = EnergyConstants.Level14EnergyReserve,
                    NextLevel = EnergyReserveLevels.Level15,
                    NextLevelCost = EnergyConstants.Level15EnergyReserveCost
                }
            },
            {
                EnergyReserveLevels.Level15, new EnergyReserveParameters
                {
                    MaxReserve = EnergyConstants.Level15EnergyReserve,
                    NextLevel = null,
                    NextLevelCost = null
                }
            }
        };
    }
}