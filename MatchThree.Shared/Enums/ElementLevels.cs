using MatchThree.Shared.Attributes;
using MatchThree.Shared.Constants;

namespace MatchThree.Shared.Enums;

public enum ElementLevels
{
    [UpgradeCost(FieldElementsConstants.Level1BaseFieldElementCost)]    //it should be here, that's the way
    Undefined = 0,
    
    [UpgradeCost(FieldElementsConstants.Level2BaseFieldElementCost)]
    Level1 = 1,
    
    [UpgradeCost(FieldElementsConstants.Level3BaseFieldElementCost)]
    Level2 = 2,

    [UpgradeCost(FieldElementsConstants.Level4BaseFieldElementCost)]
    Level3 = 3,

    [UpgradeCost(FieldElementsConstants.Level5BaseFieldElementCost)]
    Level4 = 4,

    [UpgradeConditionArgument<FieldLevels>(FieldLevels.Level4)]
    [UpgradeCost(FieldElementsConstants.Level6BaseFieldElementCost)]
    Level5 = 5,

    [UpgradeCost(FieldElementsConstants.Level7BaseFieldElementCost)]
    Level6 = 6,

    [UpgradeCost(FieldElementsConstants.Level8BaseFieldElementCost)]
    Level7 = 7,

    [UpgradeCost(FieldElementsConstants.Level9BaseFieldElementCost)]
    Level8 = 8,

    [UpgradeCost(FieldElementsConstants.Level10BaseFieldElementCost)]
    Level9 = 9,

    [UpgradeConditionArgument<FieldLevels>(FieldLevels.Level12)]
    [UpgradeCost(FieldElementsConstants.Level11BaseFieldElementCost)]
    Level10 = 10,

    [UpgradeCost(FieldElementsConstants.Level12BaseFieldElementCost)]
    Level11 = 11,

    [UpgradeCost(FieldElementsConstants.Level13BaseFieldElementCost)]
    Level12 = 12,

    [UpgradeCost(FieldElementsConstants.Level14BaseFieldElementCost)]
    Level13 = 13,

    [UpgradeCost(FieldElementsConstants.Level15BaseFieldElementCost)]
    Level14 = 14,

    [UpgradeConditionArgument<FieldLevels>(FieldLevels.Level20)]
    [UpgradeCost(FieldElementsConstants.Level16BaseFieldElementCost)]
    Level15 = 15,

    [UpgradeCost(FieldElementsConstants.Level17BaseFieldElementCost)]
    Level16 = 16,

    [UpgradeCost(FieldElementsConstants.Level18BaseFieldElementCost)]
    Level17 = 17,

    [UpgradeCost(FieldElementsConstants.Level19BaseFieldElementCost)]
    Level18 = 18,

    [UpgradeCost(FieldElementsConstants.Level20BaseFieldElementCost)]
    Level19 = 19,

    [UpgradeConditionArgument<FieldLevels>(FieldLevels.Level30)]
    [UpgradeCost(FieldElementsConstants.Level21BaseFieldElementCost)]
    Level20 = 20,

    [UpgradeCost(FieldElementsConstants.Level22BaseFieldElementCost)]
    Level21 = 21,

    [UpgradeCost(FieldElementsConstants.Level23BaseFieldElementCost)]
    Level22 = 22,

    [UpgradeCost(FieldElementsConstants.Level24BaseFieldElementCost)]
    Level23 = 23,

    [UpgradeCost(FieldElementsConstants.Level25BaseFieldElementCost)]
    Level24 = 24,

    [UpgradeConditionArgument<FieldLevels>(FieldLevels.Level38)]
    [UpgradeCost(FieldElementsConstants.Level26BaseFieldElementCost)]
    Level25 = 25,

    [UpgradeCost(FieldElementsConstants.Level27BaseFieldElementCost)]
    Level26 = 26,

    [UpgradeCost(FieldElementsConstants.Level28BaseFieldElementCost)]
    Level27 = 27,

    [UpgradeCost(FieldElementsConstants.Level29BaseFieldElementCost)]
    Level28 = 28,

    [UpgradeCost(FieldElementsConstants.Level30BaseFieldElementCost)]
    Level29 = 29,

    [UpgradeConditionArgument<FieldLevels>(FieldLevels.Level50)]
    [UpgradeCost(FieldElementsConstants.Level31BaseFieldElementCost)]
    Level30 = 30,

    [UpgradeCost(FieldElementsConstants.Level32BaseFieldElementCost)]
    Level31 = 31,

    [UpgradeCost(FieldElementsConstants.Level33BaseFieldElementCost)]
    Level32 = 32,

    [UpgradeCost(FieldElementsConstants.Level34BaseFieldElementCost)]
    Level33 = 33,

    [UpgradeCost(FieldElementsConstants.Level35BaseFieldElementCost)]
    Level34 = 34,

    [UpgradeConditionArgument<FieldLevels>(FieldLevels.Level55)]
    [UpgradeCost(FieldElementsConstants.Level36BaseFieldElementCost)]
    Level35 = 35,

    [UpgradeCost(FieldElementsConstants.Level37BaseFieldElementCost)]
    Level36 = 36,

    [UpgradeCost(FieldElementsConstants.Level38BaseFieldElementCost)]
    Level37 = 37,

    [UpgradeCost(FieldElementsConstants.Level39BaseFieldElementCost)]
    Level38 = 38,

    [UpgradeConditionArgument<FieldLevels>(FieldLevels.Level57)]
    [UpgradeCost(FieldElementsConstants.Level40BaseFieldElementCost)]
    Level39 = 39,

    Level40 = 40,
}