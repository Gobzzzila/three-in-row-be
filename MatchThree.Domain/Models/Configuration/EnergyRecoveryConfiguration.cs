using MatchThree.Shared.Constants;
using MatchThree.Shared.Enums;

namespace MatchThree.Domain.Models.Configuration;

public static class EnergyRecoveryConfiguration
{
    private static readonly Dictionary<EnergyRecoveryLevels, EnergyRecoveryParameters> EnergyRecoveryParams;
    
    static EnergyRecoveryConfiguration()
    {
        EnergyRecoveryParams = new Dictionary<EnergyRecoveryLevels, EnergyRecoveryParameters>
        {
            {
                EnergyRecoveryLevels.Level1, new EnergyRecoveryParameters
                {
                    RecoveryTime = EnergyConstants.Level1EnergyRecovery,
                    NextLevel = EnergyRecoveryLevels.Level2
                }
            },
            {
                EnergyRecoveryLevels.Level2, new EnergyRecoveryParameters
                {
                    RecoveryTime = EnergyConstants.Level2EnergyRecovery,
                    NextLevel = EnergyRecoveryLevels.Level3
                }
            },
            {
                EnergyRecoveryLevels.Level3, new EnergyRecoveryParameters
                {
                    RecoveryTime = EnergyConstants.Level3EnergyRecovery,
                    NextLevel = EnergyRecoveryLevels.Level4
                }
            },
            {
                EnergyRecoveryLevels.Level4, new EnergyRecoveryParameters
                {
                    RecoveryTime = EnergyConstants.Level4EnergyRecovery,
                    NextLevel = EnergyRecoveryLevels.Level5
                }
            },
            {
                EnergyRecoveryLevels.Level5, new EnergyRecoveryParameters
                {
                    RecoveryTime = EnergyConstants.Level5EnergyRecovery,
                    NextLevel = EnergyRecoveryLevels.Level6
                }
            },
            {
                EnergyRecoveryLevels.Level6, new EnergyRecoveryParameters
                {
                    RecoveryTime = EnergyConstants.Level6EnergyRecovery,
                    NextLevel = null
                }
            },
        };
    }
    
    public static EnergyRecoveryLevels GetStartValue()
    {
        return EnergyRecoveryLevels.Level1;
    }
}