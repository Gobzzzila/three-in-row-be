using MatchThree.Shared.Attributes;
using MatchThree.Shared.Constants;

namespace MatchThree.Shared.Enums;

public enum UpgradeTypes
{
    [TranslationId(TranslationConstants.UndefinedTextKey)]
    Undefined = 0,
    
    [TranslationId(TranslationConstants.UpgradeEnergyReserveHeaderKey)]
    EnergyReserve = 1,
    
    [TranslationId(TranslationConstants.UpgradeEnergyRecoveryHeaderKey)]
    EnergyRecovery = 2,
    
    [TranslationId(TranslationConstants.UpgradeFieldHeaderKey)]
    Field = 3,
}