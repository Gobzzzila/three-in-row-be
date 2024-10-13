using MatchThree.Shared.Attributes;
using MatchThree.Shared.Constants;

namespace MatchThree.Shared.Enums;

public enum UpgradeCategories
{
    [TranslationId(TranslationConstants.UndefinedTextKey)]
    Undefined = 0,
    
    [TranslationId(TranslationConstants.EnergyCategoryTextKey)]
    Energy = 1,
    
    [TranslationId(TranslationConstants.FieldCategoryTextKey)]
    Field = 2,
}