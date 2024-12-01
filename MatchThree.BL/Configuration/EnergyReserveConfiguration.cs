using System.Collections.Frozen;
using MatchThree.Domain.Configuration;
using MatchThree.Domain.Interfaces.Upgrades;
using MatchThree.Shared.Constants;
using MatchThree.Shared.Enums;
using MatchThree.Shared.Extensions;

namespace MatchThree.BL.Configuration;

public static class EnergyReserveConfiguration
{
    private static readonly FrozenDictionary<EnergyReserveLevels, EnergyReserveParameters> EnergyReserveParams;
    
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
        var enumValues = Enum.GetValues<EnergyReserveLevels>();
        var dictionary = new Dictionary<EnergyReserveLevels, EnergyReserveParameters>(enumValues.Length - 1);
        
        for (var i = 1; i < enumValues.Length; i++)
        {
            var currentValue = enumValues[i];
            var upgradeConditionArg = currentValue.GetUpgradeConditionArgument<int, EnergyReserveLevels>();
            dictionary.Add(currentValue, new EnergyReserveParameters
            {
                MaxReserve = EnergyConstants.Level0EnergyReserve + (i * EnergyConstants.EnergyReserveMultiplierPerLevel),
                NextLevelCost = currentValue.GetUpgradeCost(),
                NextLevel = enumValues.Length - 1 != i ? 
                    enumValues[i + 1] : 
                    null,
                UpgradeCondition = upgradeConditionArg != 0 ? 
                    UpgradeCondition(upgradeConditionArg) : 
                    null
            });
        }

        EnergyReserveParams = dictionary.ToFrozenDictionary();
    }

    private static Func<IUpgradesRestrictionsService, long, Task<int?>> UpgradeCondition(int requiredReferralsAmount)
    {
        return (upgradesRestrictionsService, userId) => 
            upgradesRestrictionsService.CalculateNumberOfMissingReferralsAsync(userId, requiredReferralsAmount);
    }
}