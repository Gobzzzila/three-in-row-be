using MatchThree.Domain.Configuration;
using MatchThree.Shared.Constants;
using MatchThree.Shared.Enums;
using MatchThree.Shared.Extensions;

namespace MatchThree.BL.Configuration;

public static class FieldConfiguration
{
    private static readonly Dictionary<FieldLevels, FieldParameters> FieldParams;

    static FieldConfiguration()
    {
        var enumValues = Enum.GetValues(typeof(FieldLevels));
        FieldParams = new Dictionary<FieldLevels, FieldParameters>(enumValues.Length - 1);
        
        for (var i = 1; i < enumValues.Length; i++)
        {
            var currentValue = (FieldLevels)enumValues.GetValue(i)!;
            FieldParams.Add(currentValue, new FieldParameters
            {
                NextLevelCost = currentValue.GetUpgradeCost(),
                AmountOfCells = FieldConstants.BaseAmountOfCells + i - 1,
                NextLevel = 
                    i != enumValues.Length - 1 ? 
                        (FieldLevels)enumValues.GetValue(i + 1)! :
                        null
            });
        }
    }
    
    public static FieldLevels GetStartValue()
    {
        return FieldLevels.Level1;
    }
}