using MatchThree.Shared.Attributes;
using MatchThree.Shared.Constants;

namespace MatchThree.Shared.Enums;

public enum UpgradeCategories
{
    [TranslationId(TranslationConstants.CommonUndefinedTextKey)]
    Undefined = 0,
    
    [TranslationId(TranslationConstants.UpgradeCategoryEnergyTextKey)]
    Energy = 1,
    
    [TranslationId(TranslationConstants.UpgradeCategoryFieldTextKey)]
    Field = 2,
}