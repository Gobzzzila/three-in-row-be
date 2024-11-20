using MatchThree.Shared.Attributes;
using MatchThree.Shared.Constants;

namespace MatchThree.Shared.Enums;

public enum FieldLevels
{
    Undefined = 0,
    
    [UpgradeCost(FieldConstants.Level2FieldCost)]
    [NextLevelFieldInfo(1, 0)]
    Level1 = 1,
    
    [UpgradeCost(FieldConstants.Level3FieldCost)]
    [NextLevelFieldInfo(1, 1)]
    Level2 = 2,
    
    [UpgradeCost(FieldConstants.Level4FieldCost)]
    [NextLevelFieldInfo(1, 2)]
    Level3 = 3,
    
    [UpgradeCost(FieldConstants.Level5FieldCost)]
    [NextLevelFieldInfo(1, 3)]
    Level4 = 4,
    
    [UpgradeCost(FieldConstants.Level6FieldCost)]
    [NextLevelFieldInfo(1, 4)]
    Level5 = 5,
    
    [UpgradeCost(FieldConstants.Level7FieldCost)]
    [NextLevelFieldInfo(1, 5)]
    Level6 = 6,
    
    [UpgradeCost(FieldConstants.Level8FieldCost)]
    [NextLevelFieldInfo(2, 5)]
    Level7 = 7,
    
    [UpgradeCost(FieldConstants.Level9FieldCost)]
    [NextLevelFieldInfo(3, 5)]
    Level8 = 8,
    
    [UpgradeCost(FieldConstants.Level10FieldCost)]
    [NextLevelFieldInfo(4, 5)]
    Level9 = 9,
    
    [UpgradeCost(FieldConstants.Level11FieldCost)]
    [NextLevelFieldInfo(5, 5)]
    Level10 = 10,
    
    [UpgradeCost(FieldConstants.Level12FieldCost)]
    [NextLevelFieldInfo(6, 5, CryptoTypes.Jetton)]
    Level11 = 11,
    
    [UpgradeCost(FieldConstants.Level13FieldCost)]
    [NextLevelFieldInfo(7, 0)]
    Level12 = 12,
    
    [UpgradeCost(FieldConstants.Level14FieldCost)]
    [NextLevelFieldInfo(7, 1)]
    Level13 = 13,
    
    [UpgradeCost(FieldConstants.Level15FieldCost)]
    [NextLevelFieldInfo(7, 2)]
    Level14 = 14,
    
    [UpgradeCost(FieldConstants.Level16FieldCost)]
    [NextLevelFieldInfo(7, 3)]
    Level15 = 15,
    
    [UpgradeCost(FieldConstants.Level17FieldCost)]
    [NextLevelFieldInfo(7, 4)]
    Level16 = 16,
    
    [UpgradeCost(FieldConstants.Level18FieldCost)]
    [NextLevelFieldInfo(7, 5)]
    Level17 = 17,
    
    [UpgradeCost(FieldConstants.Level19FieldCost)]
    [NextLevelFieldInfo(7, 6)]
    Level18 = 18,
    
    [UpgradeCost(FieldConstants.Level20FieldCost)]
    [NextLevelFieldInfo(6, 6)]
    Level19 = 19,
    
    [UpgradeCost(FieldConstants.Level21FieldCost)]
    [NextLevelFieldInfo(5, 6)]
    Level20 = 20,
    
    [UpgradeCost(FieldConstants.Level22FieldCost)]
    [NextLevelFieldInfo(4, 6)]
    Level21 = 21,
    
    [UpgradeCost(FieldConstants.Level23FieldCost)]
    [NextLevelFieldInfo(3, 6)]
    Level22 = 22,
    
    [UpgradeCost(FieldConstants.Level24FieldCost)]
    [NextLevelFieldInfo(2, 6)]
    Level23 = 23,
    
    [UpgradeCost(FieldConstants.Level25FieldCost)]
    [NextLevelFieldInfo(1, 6, CryptoTypes.Not)]
    Level24 = 24,
    
    [UpgradeCost(FieldConstants.Level26FieldCost)]
    [NextLevelFieldInfo(0, 0)]
    Level25 = 25,
    
    [UpgradeCost(FieldConstants.Level27FieldCost)]
    [NextLevelFieldInfo(0, 1)]
    Level26 = 26,
    
    [UpgradeCost(FieldConstants.Level28FieldCost)]
    [NextLevelFieldInfo(0, 2)]
    Level27 = 27,
    
    [UpgradeCost(FieldConstants.Level29FieldCost)]
    [NextLevelFieldInfo(0, 3)]
    Level28 = 28,
    
    [UpgradeCost(FieldConstants.Level30FieldCost)]
    [NextLevelFieldInfo(0, 4)]
    Level29 = 29,
    
    [UpgradeCost(FieldConstants.Level31FieldCost)]
    [NextLevelFieldInfo(0, 5)]
    Level30 = 30,
    
    [UpgradeCost(FieldConstants.Level32FieldCost)]
    [NextLevelFieldInfo(0, 6)]
    Level31 = 31,
    
    [UpgradeCost(FieldConstants.Level33FieldCost)]
    [NextLevelFieldInfo(0, 7)]
    Level32 = 32,
    
    [UpgradeCost(FieldConstants.Level34FieldCost)]
    [NextLevelFieldInfo(1, 7)]
    Level33 = 33,
    
    [UpgradeCost(FieldConstants.Level35FieldCost)]
    [NextLevelFieldInfo(2, 7)]
    Level34 = 34,
    
    [UpgradeCost(FieldConstants.Level36FieldCost)]
    [NextLevelFieldInfo(3, 7)]
    Level35 = 35,
    
    [UpgradeCost(FieldConstants.Level37FieldCost)]
    [NextLevelFieldInfo(4, 7)]
    Level36 = 36,
    
    [UpgradeCost(FieldConstants.Level38FieldCost)]
    [NextLevelFieldInfo(5, 7)]
    Level37 = 37,
    
    [UpgradeCost(FieldConstants.Level39FieldCost)]
    [NextLevelFieldInfo(6, 7)]
    Level38 = 38,
    
    [UpgradeCost(FieldConstants.Level40FieldCost)]
    [NextLevelFieldInfo(7, 7, CryptoTypes.Dogs)]
    Level39 = 39,
    
    [UpgradeCost(FieldConstants.Level41FieldCost)]
    [NextLevelFieldInfo(8, 0)]
    Level40 = 40,
    
    [UpgradeCost(FieldConstants.Level42FieldCost)]
    [NextLevelFieldInfo(8, 1)]
    Level41 = 41,
    
    [UpgradeCost(FieldConstants.Level43FieldCost)]
    [NextLevelFieldInfo(8, 2)]
    Level42 = 42,
    
    [UpgradeCost(FieldConstants.Level44FieldCost)]
    [NextLevelFieldInfo(8, 3)]
    Level43 = 43,
    
    [UpgradeCost(FieldConstants.Level45FieldCost)]
    [NextLevelFieldInfo(8, 4)]
    Level44 = 44,
    
    [UpgradeCost(FieldConstants.Level46FieldCost)]
    [NextLevelFieldInfo(8, 5)]
    Level45 = 45,
    
    [UpgradeCost(FieldConstants.Level47FieldCost)]
    [NextLevelFieldInfo(8, 6)]
    Level46 = 46,
    
    [UpgradeCost(FieldConstants.Level48FieldCost)]
    [NextLevelFieldInfo(8, 7)]
    Level47 = 47,
    
    [UpgradeCost(FieldConstants.Level49FieldCost)]
    [NextLevelFieldInfo(8, 8)]
    Level48 = 48,
    
    [UpgradeCost(FieldConstants.Level50FieldCost)]
    [NextLevelFieldInfo(7, 8)]
    Level49 = 49,
    
    [UpgradeCost(FieldConstants.Level51FieldCost)]
    [NextLevelFieldInfo(6, 8)]
    Level50 = 50,
    
    [UpgradeCost(FieldConstants.Level52FieldCost)]
    [NextLevelFieldInfo(5, 8)]
    Level51 = 51,
    
    [UpgradeCost(FieldConstants.Level53FieldCost)]
    [NextLevelFieldInfo(4, 8)]
    Level52 = 52, 
    
    [UpgradeCost(FieldConstants.Level54FieldCost)]
    [NextLevelFieldInfo(3, 8)]
    Level53 = 53,
    
    [UpgradeCost(FieldConstants.Level55FieldCost)]
    [NextLevelFieldInfo(2, 8)]
    Level54 = 54,
    
    [UpgradeCost(FieldConstants.Level56FieldCost)]
    [NextLevelFieldInfo(1, 8)]
    Level55 = 55,
    
    [UpgradeCost(FieldConstants.Level57FieldCost)]
    [NextLevelFieldInfo(0, 8, CryptoTypes.Cati)]
    Level56 = 56,
    
    [NextLevelFieldInfo]    //it should be here, that's the way
    Level57 = 57
}