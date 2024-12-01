using System.Collections.Frozen;
using MatchThree.Domain.Configuration;
using MatchThree.Domain.Interfaces.Upgrades;
using MatchThree.Shared.Constants;
using MatchThree.Shared.Enums;
using MatchThree.Shared.Extensions;

namespace MatchThree.BL.Configuration;

public static class FieldElementsConfiguration
{
    private static readonly FrozenDictionary<CryptoTypes, FrozenDictionary<ElementLevels, FieldElementParameters>> FieldElementsParams;
    
    public static FieldElementParameters GetParamsByTypeAndLevel(CryptoTypes cryptoType, ElementLevels elementLevel)
    {
        return FieldElementsParams[cryptoType][elementLevel];
    }
    
    public static int GetProfit(CryptoTypes cryptoType, ElementLevels elementLevel)
    {
        return FieldElementsParams[cryptoType][elementLevel].Profit;
    }
    
    public static int? GetNextLevelProfit(CryptoTypes cryptoType, ElementLevels elementLevel)
    {
        var fieldLevelParams = GetParamsByTypeAndLevel(cryptoType, elementLevel);
        return fieldLevelParams.NextLevel is not null
            ? FieldElementsParams[cryptoType][fieldLevelParams.NextLevel!.Value].Profit
            : default(int?);
    }
    
    public static List<(CryptoTypes cryptoType, ElementLevels elementLevel)> GetStartValue()
    {
        return [
            (CryptoTypes.Ton, ElementLevels.Level1),
            (CryptoTypes.Jetton, ElementLevels.Level1),
            (CryptoTypes.Ston, ElementLevels.Level1),
            (CryptoTypes.Raff, ElementLevels.Level1),
            (CryptoTypes.Not, ElementLevels.Level1),
            (CryptoTypes.Usdt, ElementLevels.Undefined),
            (CryptoTypes.Fnz, ElementLevels.Undefined),
            (CryptoTypes.Dogs, ElementLevels.Undefined),
            (CryptoTypes.Cati, ElementLevels.Undefined)
        ];
    }

    public static FieldLevels GetRequiredFieldLevelForFirstLevelElement(CryptoTypes cryptoType)
    {
        return cryptoType switch
        {
            CryptoTypes.Usdt => FieldLevels.Level12,
            CryptoTypes.Fnz => FieldLevels.Level25,
            CryptoTypes.Dogs => FieldLevels.Level40,
            CryptoTypes.Cati => FieldLevels.Level57,
            _ => FieldLevels.Undefined
        };
    }
    
    private record MultiplierAndSyllable(double Multiplier, int Syllable);
    static FieldElementsConfiguration()
    {
        var cryptoTypes = Enum.GetValues<CryptoTypes>();
        var elementLevels = Enum.GetValues<ElementLevels>();

        var dictionary = 
            new Dictionary<CryptoTypes, FrozenDictionary<ElementLevels, FieldElementParameters>>(cryptoTypes.Length - 1);

        var multipliersAndSyllables = new Dictionary<CryptoTypes, MultiplierAndSyllable>
        {
            { CryptoTypes.Usdt, new MultiplierAndSyllable(FieldElementsConstants.UsdtUpgradeCostMultiplier, FieldElementsConstants.UsdtProfitSyllable) },
            { CryptoTypes.Fnz, new MultiplierAndSyllable(FieldElementsConstants.FnzUpgradeCostMultiplier, FieldElementsConstants.FnzProfitSyllable) },
            { CryptoTypes.Dogs, new MultiplierAndSyllable(FieldElementsConstants.DogsUpgradeCostMultiplier, FieldElementsConstants.DogsProfitSyllable) },
            { CryptoTypes.Cati, new MultiplierAndSyllable(FieldElementsConstants.CatiUpgradeCostMultiplier, FieldElementsConstants.CatiProfitSyllable) }
        };

        for (var i = 1; i < cryptoTypes.Length; i++)
        {
            var cryptoTypeDictionary = new Dictionary<ElementLevels, FieldElementParameters>(elementLevels.Length - 1);
            var currentCryptoType = cryptoTypes[i];
            
            var multiplierAndSyllable = new MultiplierAndSyllable(1.0, 0);
            if (multipliersAndSyllables.TryGetValue(currentCryptoType, out var value))
                multiplierAndSyllable = value;

            var jStartValue = GetRequiredFieldLevelForFirstLevelElement(currentCryptoType) == FieldLevels.Undefined ? 1 : 0;
            for (var j = jStartValue; j < elementLevels.Length; j++)
            {
                var currentLevel = (ElementLevels)elementLevels.GetValue(j)!;
                var upgradeConditionArg = currentLevel.GetUpgradeConditionArgument<FieldLevels, ElementLevels>();
                var fieldElementParameters = new FieldElementParameters
                {
                    Profit = (int)currentLevel + (currentLevel == 0 ? 0 : multiplierAndSyllable.Syllable),
                    NextLevelCost = (uint?)(currentLevel.GetUpgradeCost() * multiplierAndSyllable.Multiplier),
                    NextLevel = elementLevels.Length - 1 != j ? 
                        elementLevels[j + 1] : 
                        null,
                    UpgradeCondition = upgradeConditionArg != FieldLevels.Undefined ? 
                        UpgradeCondition(upgradeConditionArg) : 
                        null 
                };

                cryptoTypeDictionary.Add(currentLevel, fieldElementParameters);
            }
            dictionary.Add(currentCryptoType, cryptoTypeDictionary.ToFrozenDictionary());
        }

        FieldElementsParams = dictionary.ToFrozenDictionary();
    }
    
    private static Func<IUpgradesRestrictionsService, long, Task<int?>> UpgradeCondition(FieldLevels restrictedFieldLevel)
    {
        return (upgradesRestrictionsService, userId) => 
            upgradesRestrictionsService.ValidateFieldLevel(userId, restrictedFieldLevel);
    }
}