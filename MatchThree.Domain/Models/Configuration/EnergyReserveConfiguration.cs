using MatchThree.Shared.Constants;
using MatchThree.Shared.Enums;

namespace MatchThree.Domain.Models.Configuration;

public static class EnergyReserveConfiguration
{
    private static readonly Dictionary<EnergyReserveLevels, EnergyReserveParameters> EnergyReservesParams;

    static EnergyReserveConfiguration()
    {
        EnergyReservesParams = new Dictionary<EnergyReserveLevels, EnergyReserveParameters>
        {
            {
                EnergyReserveLevels.Level1, new EnergyReserveParameters
                {
                    MaxValue = EnergyConstants.Level1EnergyReserve,
                    NextLevel = EnergyReserveLevels.Level2
                }
            },
            {
                EnergyReserveLevels.Level2, new EnergyReserveParameters
                {
                    MaxValue = EnergyConstants.Level2EnergyReserve,
                    NextLevel = EnergyReserveLevels.Level3
                }
            },
            {
                EnergyReserveLevels.Level3, new EnergyReserveParameters
                {
                    MaxValue = EnergyConstants.Level3EnergyReserve,
                    NextLevel = EnergyReserveLevels.Level4
                }
            },
            {
                EnergyReserveLevels.Level4, new EnergyReserveParameters
                {
                    MaxValue = EnergyConstants.Level4EnergyReserve,
                    NextLevel = EnergyReserveLevels.Level5
                }
            },
            {
                EnergyReserveLevels.Level5, new EnergyReserveParameters
                {
                    MaxValue = EnergyConstants.Level5EnergyReserve,
                    NextLevel = EnergyReserveLevels.Level6
                }
            },
            {
                EnergyReserveLevels.Level6, new EnergyReserveParameters
                {
                    MaxValue = EnergyConstants.Level6EnergyReserve,
                    NextLevel = EnergyReserveLevels.Level7
                }
            },
            {
                EnergyReserveLevels.Level7, new EnergyReserveParameters
                {
                    MaxValue = EnergyConstants.Level7EnergyReserve,
                    NextLevel = EnergyReserveLevels.Level8
                }
                
            },
            {
                EnergyReserveLevels.Level8, new EnergyReserveParameters
                {
                    MaxValue = EnergyConstants.Level8EnergyReserve,
                    NextLevel = EnergyReserveLevels.Level9
                }
            },
            {
                EnergyReserveLevels.Level9, new EnergyReserveParameters
                {
                    MaxValue = EnergyConstants.Level9EnergyReserve,
                    NextLevel = EnergyReserveLevels.Level10
                }
            },
            {
                EnergyReserveLevels.Level10, new EnergyReserveParameters
                {
                    MaxValue = EnergyConstants.Level10EnergyReserve,
                    NextLevel = EnergyReserveLevels.Level11
                }
            },
            {
                EnergyReserveLevels.Level11, new EnergyReserveParameters
                {
                    MaxValue = EnergyConstants.Level11EnergyReserve,
                    NextLevel = EnergyReserveLevels.Level12
                }
            },
            {
                EnergyReserveLevels.Level12, new EnergyReserveParameters
                {
                    MaxValue = EnergyConstants.Level12EnergyReserve,
                    NextLevel = EnergyReserveLevels.Level13
                }
            },
            {
                EnergyReserveLevels.Level13, new EnergyReserveParameters
                {
                    MaxValue = EnergyConstants.Level13EnergyReserve,
                    NextLevel = EnergyReserveLevels.Level14
                }
            },
            {
                EnergyReserveLevels.Level14, new EnergyReserveParameters
                {
                    MaxValue = EnergyConstants.Level14EnergyReserve,
                    NextLevel = EnergyReserveLevels.Level15
                }
            },
            {
                EnergyReserveLevels.Level15, new EnergyReserveParameters
                {
                    MaxValue = EnergyConstants.Level15EnergyReserve,
                    NextLevel = null
                }
            }
        };
    }
    
    public static (EnergyReserveLevels ReserveLevel, int ReserveValue) GetStartValue()
    {
        return (EnergyReserveLevels.Level1, EnergyReservesParams[EnergyReserveLevels.Level1].MaxValue);
    }
    
    public static int GetReserveMaxValue(EnergyReserveLevels energyReserveLevel)
    {
        return EnergyReservesParams[energyReserveLevel].MaxValue;
    }
}