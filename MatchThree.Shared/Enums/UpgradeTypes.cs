using MatchThree.Shared.Attributes;
using MatchThree.Shared.Constants;

namespace MatchThree.Shared.Enums;

public enum UpgradeTypes
{
    [UpgradeInfo(TranslationConstants.UndefinedTextKey,
        TranslationConstants.UndefinedTextKey,
        TranslationConstants.UndefinedTextKey)]
    Undefined = 0,
    
    [UpgradeInfo(TranslationConstants.UpgradeEnergyReserveHeaderKey,
        TranslationConstants.UpgradeEnergyReserveDescriptionKey,
        TranslationConstants.UpgradeEnergyReserveBlockingTextKey)]
    EnergyReserve = 1,
    
    [UpgradeInfo(TranslationConstants.UpgradeEnergyRecoveryHeaderKey,
        TranslationConstants.UpgradeEnergyRecoveryDescriptionKey,
        TranslationConstants.UpgradeEnergyRecoveryBlockingTextKey)]
    EnergyRecovery = 2,
    
    [UpgradeInfo(TranslationConstants.UpgradeFieldHeaderKey,
        TranslationConstants.UpgradeFieldDescriptionKey)]
    Field = 3,

    [UpgradeInfo(TranslationConstants.EnergyDrinkHeaderKey,
        TranslationConstants.EnergyDrinkDescriptionKey,
        TranslationConstants.EnergyDrinkBlockingTextKey)]
    EnergyDrink = 4, 
}