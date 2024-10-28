using MatchThree.Domain.Configuration;
using MatchThree.Shared.Enums;
using MatchThree.Shared.Extensions;

namespace MatchThree.BL.Configuration;

public static class FieldElementsConfiguration
{
    private static readonly Dictionary<CryptoTypes, Dictionary<ElementLevels, FieldElementParameters>> FieldElemetsParams;

    static FieldElementsConfiguration()
    {
        var cryptoTypes = Enum.GetValues(typeof(CryptoTypes));
        var elementLevels = Enum.GetValues(typeof(CryptoTypes));

        FieldElemetsParams = 
            new Dictionary<CryptoTypes, Dictionary<ElementLevels, FieldElementParameters>>(cryptoTypes.Length - 1);
        
        for (var i = 1; i < 6; i++)
        {
            var currentCryptoType = (CryptoTypes)cryptoTypes.GetValue(i)!;
            var cryptoTypeDictionary = new Dictionary<ElementLevels, FieldElementParameters>(elementLevels.Length - 1);

            for (var j = 1; j < elementLevels.Length; j++)
            {
                var currentLevel = (ElementLevels)elementLevels.GetValue(j)!;

                var fieldElementParameters = new FieldElementParameters
                {
                    Profit = (int)currentLevel,
                    NextLevelCost = currentLevel.GetUpgradeCost(),
                    NextLevel = 
                        i != elementLevels.Length - 1 
                            ? (ElementLevels)elementLevels.GetValue(i + 1)!
                            : null
                };
                
                cryptoTypeDictionary.Add(currentLevel, fieldElementParameters);
            }
            
            FieldElemetsParams.Add(currentCryptoType, cryptoTypeDictionary);
        }
    }
}