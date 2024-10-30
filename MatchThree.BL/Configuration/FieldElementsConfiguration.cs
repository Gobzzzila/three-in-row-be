using MatchThree.Domain.Configuration;
using MatchThree.Shared.Constants;
using MatchThree.Shared.Enums;
using MatchThree.Shared.Extensions;

namespace MatchThree.BL.Configuration;

public static class FieldElementsConfiguration
{
    private static readonly Dictionary<CryptoTypes, Dictionary<ElementLevels, FieldElementParameters>> FieldElemetsParams;

    public static List<(CryptoTypes cryptoType, ElementLevels elementLevel)> GetStartValue()
    {
        return [
            (CryptoTypes.Ton, ElementLevels.Level1),
            (CryptoTypes.Ston, ElementLevels.Level1),
            (CryptoTypes.Mc, ElementLevels.Level1),
            (CryptoTypes.Fnz, ElementLevels.Level1),
            (CryptoTypes.Usdt, ElementLevels.Level1),
            (CryptoTypes.Jetton, ElementLevels.Undefined),
            (CryptoTypes.Not, ElementLevels.Undefined),
            (CryptoTypes.Dogs, ElementLevels.Undefined),
            (CryptoTypes.Cati, ElementLevels.Undefined)
        ];
    }
    
    public static int GetProfit(CryptoTypes cryptoType, ElementLevels elementLevel)
    {
        return FieldElemetsParams[cryptoType][elementLevel].Profit;
    }
    
    private record MultiplierAndSyllable(double Multiplier, int Syllable);
    static FieldElementsConfiguration()
    {
        var cryptoTypes = Enum.GetValues(typeof(CryptoTypes));
        var elementLevels = Enum.GetValues(typeof(ElementLevels));

        FieldElemetsParams = 
            new Dictionary<CryptoTypes, Dictionary<ElementLevels, FieldElementParameters>>(cryptoTypes.Length - 1);

        var multipliersAndSyllables = new Dictionary<CryptoTypes, MultiplierAndSyllable>
        {
            { CryptoTypes.Jetton, new MultiplierAndSyllable(FieldElementsConstants.JettonUpgradeCostMultiplier, FieldElementsConstants.JettonProfitSyllable) },
            { CryptoTypes.Not, new MultiplierAndSyllable(FieldElementsConstants.NotUpgradeCostMultiplier, FieldElementsConstants.NotProfitSyllable) },
            { CryptoTypes.Dogs, new MultiplierAndSyllable(FieldElementsConstants.DogsUpgradeCostMultiplier, FieldElementsConstants.DogsProfitSyllable) },
            { CryptoTypes.Cati, new MultiplierAndSyllable(FieldElementsConstants.CatiUpgradeCostMultiplier, FieldElementsConstants.CatiProfitSyllable) }
        };

        for (var i = 1; i < cryptoTypes.Length; i++)
        {
            var cryptoTypeDictionary = new Dictionary<ElementLevels, FieldElementParameters>(elementLevels.Length - 1);
            var currentCryptoType = (CryptoTypes)cryptoTypes.GetValue(i)!;
            
            var multiplierAndSyllable = new MultiplierAndSyllable(1.0, 0);
            if (multipliersAndSyllables.TryGetValue(currentCryptoType, out var value))
                multiplierAndSyllable = value;
            
            var jStartValue = i > 5 ? 0 : 1;
            for (var j = jStartValue; j < elementLevels.Length; j++)
            {
                var currentLevel = (ElementLevels)elementLevels.GetValue(j)!;
                var fieldElementParameters = new FieldElementParameters
                {
                    Profit = (int)currentLevel + (currentLevel == 0 ? 0 : multiplierAndSyllable.Syllable),
                    NextLevelCost = (uint?)(currentLevel.GetUpgradeCost() * multiplierAndSyllable.Multiplier),
                    NextLevel = 
                        j != elementLevels.Length - 1 
                            ? (ElementLevels)elementLevels.GetValue(j + 1)!
                            : null
                };
                
                cryptoTypeDictionary.Add(currentLevel, fieldElementParameters);
            }
            FieldElemetsParams.Add(currentCryptoType, cryptoTypeDictionary);
        }
    }
}