using System.Collections.Frozen;
using MatchThree.Domain.Configuration;
using MatchThree.Shared.Constants;
using MatchThree.Shared.Enums;
using MatchThree.Shared.Extensions;

namespace MatchThree.BL.Configuration;

public static class FieldConfiguration
{
    private static readonly FrozenDictionary<FieldLevels, FieldParameters> FieldParams;
    
    public static FieldParameters GetParamsByLevel(FieldLevels fieldLevel)
    {
        return FieldParams[fieldLevel];
    }
    
    public static FieldLevels GetStartValue()
    {
        return FieldLevels.Level1;
    }
    
    static FieldConfiguration()
    {
        var enumValues = Enum.GetValues(typeof(FieldLevels));
        var dictionary = new Dictionary<FieldLevels, FieldParameters>(enumValues.Length - 1);
        
        for (var i = 1; i < enumValues.Length; i++)
        {
            var currentValue = (FieldLevels)enumValues.GetValue(i)!;
            var nextLevelInfo = currentValue.GetNextLevelCoordinates();
            dictionary.Add(currentValue, new FieldParameters
            {
                NextLevelCost = currentValue.GetUpgradeCost(),
                NextLevelCoordinates = (nextLevelInfo!.X, nextLevelInfo!.Y),
                NextLevelNewCrypto = nextLevelInfo.NewCrypto,
                AmountOfCells = FieldConstants.BaseAmountOfCells + i - 1,
                NextLevel = 
                    i != enumValues.Length - 1 ? 
                        (FieldLevels)enumValues.GetValue(i + 1)! :
                        null
            });
        }

        FieldParams = dictionary.ToFrozenDictionary();
    }
}