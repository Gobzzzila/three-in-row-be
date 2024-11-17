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
    
    [UpgradeInfo(TranslationConstants.UpgradeFieldElementHeaderTextKey,
        TranslationConstants.UpgradeFieldElementDescriptionTextKey,
        TranslationConstants.UpgradeFieldElementBlockingTextKey)]
    TonElement = 5, 
    
    [UpgradeInfo(TranslationConstants.UpgradeFieldElementHeaderTextKey,
        TranslationConstants.UpgradeFieldElementDescriptionTextKey,
        TranslationConstants.UpgradeFieldElementBlockingTextKey)]
    StonElement = 6, 
    
    [UpgradeInfo(TranslationConstants.UpgradeFieldElementHeaderTextKey,
        TranslationConstants.UpgradeFieldElementDescriptionTextKey,
        TranslationConstants.UpgradeFieldElementBlockingTextKey)]
    RaffElement = 7, 
    
    [UpgradeInfo(TranslationConstants.UpgradeFieldElementHeaderTextKey,
        TranslationConstants.UpgradeFieldElementDescriptionTextKey,
        TranslationConstants.UpgradeFieldElementBlockingTextKey)]
    FnzElement = 8, 
    
    [UpgradeInfo(TranslationConstants.UpgradeFieldElementHeaderTextKey,
        TranslationConstants.UpgradeFieldElementDescriptionTextKey,
        TranslationConstants.UpgradeFieldElementBlockingTextKey)]
    UsdtElement = 9, 
    
    [UpgradeInfo(TranslationConstants.UpgradeFieldElementHeaderTextKey,
        TranslationConstants.UpgradeFieldElementDescriptionTextKey,
        TranslationConstants.UpgradeFieldElementBlockingTextKey)]
    JettonElement = 10, 
    
    [UpgradeInfo(TranslationConstants.UpgradeFieldElementHeaderTextKey,
        TranslationConstants.UpgradeFieldElementDescriptionTextKey,
        TranslationConstants.UpgradeFieldElementBlockingTextKey)]
    NotElement = 11, 
    
    [UpgradeInfo(TranslationConstants.UpgradeFieldElementHeaderTextKey,
        TranslationConstants.UpgradeFieldElementDescriptionTextKey,
        TranslationConstants.UpgradeFieldElementBlockingTextKey)]
    DogsElement = 12, 
    
    [UpgradeInfo(TranslationConstants.UpgradeFieldElementHeaderTextKey,
        TranslationConstants.UpgradeFieldElementDescriptionTextKey,
        TranslationConstants.UpgradeFieldElementBlockingTextKey)]
    CatiElement = 13, 
}