using MatchThree.Shared.Attributes;
using MatchThree.Shared.Constants;

namespace MatchThree.Shared.Enums;

public enum UpgradeTypes
{
    [UpgradeInfo(TranslationConstants.CommonUndefinedTextKey,
        TranslationConstants.CommonUndefinedTextKey,
        TranslationConstants.CommonUndefinedTextKey)]
    Undefined = 0,
    
    [UpgradeInfo(TranslationConstants.UpgradeEnergyReserveHeaderTextKey,
        TranslationConstants.UpgradeEnergyReserveDescriptionTextKey,
        TranslationConstants.UpgradeEnergyReserveBlockingTextKey)]
    EnergyReserve = 1,
    
    [UpgradeInfo(TranslationConstants.UpgradeEnergyRecoveryHeaderTextKey,
        TranslationConstants.UpgradeEnergyRecoveryDescriptionTextKey,
        TranslationConstants.UpgradeEnergyRecoveryBlockingTextKey)]
    EnergyRecovery = 2,
    
    [UpgradeInfo(TranslationConstants.UpgradeFieldHeaderTextKey,
        TranslationConstants.UpgradeFieldDescriptionTextKey)]
    Field = 3,

    [UpgradeInfo(TranslationConstants.UpgradeEnergyDrinkHeaderTextKey,
        TranslationConstants.UpgradeEnergyDrinkDescriptionTextKey,
        TranslationConstants.UpgradeEnergyDrinkBlockingTextKey)]
    EnergyDrink = 4, 
}