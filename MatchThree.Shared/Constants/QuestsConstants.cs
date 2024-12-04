namespace MatchThree.Shared.Constants;

public static class QuestsConstants
{
    //Invite Friend Quests
    public const string QuestInviteFriendPictureName = "friends_1.png";

    public static readonly Guid QuestIdInvite1Friend = Guid.Parse("F1111111-908A-4619-8868-F8310EA7D2E3");
    public const uint QuestInvite1FriendReward = 1_000;
    
    public static readonly Guid QuestIdInvite3Friend = Guid.Parse("F3333333-908A-4619-8868-F8310EA7D2E3");
    public const uint QuestInvite3FriendReward = 4_000;
    
    public static readonly Guid QuestIdInvite5Friend = Guid.Parse("F5555555-908A-4619-8868-F8310EA7D2E3");
    public const uint QuestInvite5FriendReward = 10_000;
    
    //Tg Subscription Quests
    public const string QuestTgSubscriptionPictureName = "TgSubscriptionQuest.png";

    public static readonly Guid QuestIdPingwinNewsChannelSubscription = Guid.Parse("FCA11111-908A-4619-8868-F8310EA7D2E3");
    public const uint QuestPingwinNewsChannelSubscriptionReward = 2_500;
    
    public static readonly Guid QuestIdPingwinGroupChatSubscription = Guid.Parse("FCA22222-908A-4619-8868-F8310EA7D2E3");
    public const uint QuestPingwinGroupChatSubscriptionReward = 2_500;
    
    //Be premium user Quests
    public const string QuestBePremiumUserPictureName = "PremiumUserQuest.png";

    public static readonly Guid QuestIdBePremiumUser = Guid.Parse("9FC89FF1-54A7-417A-A97B-8AFBB13FFEB9");
    public const uint QuestBePremiumUserReward = 4_000;
    
    //Mystery Code Quests
    public const string QuestMysteryCodePictureName = "MysteryCodeQuest.png";

    public static readonly Guid QuestIdWelcomeMysteryCode = Guid.Parse("E57EAF7E-0DCA-41C9-A9F1-7035CE30242B");
    public const uint QuestWelcomeMysteryCodeReward = 5_000;
    public const string QuestWelcomeMysteryCodeSecretCode = "LETSGO";
    
    //Energy Drink Purchaser Quests
    public const string QuestEnergyDrinkPurchaserPictureName = "EnergyDrinkPurchaserQuest.png";

    public static readonly Guid QuestIdPurchase1EnergyDrink = Guid.Parse("ED111111-FCB6-4A95-9064-BB0BF8C0716D");
    public const uint QuestPurchase1EnergyDrinkReward = 5_000;
    
    public static readonly Guid QuestIdPurchase3EnergyDrinks = Guid.Parse("ED333333-FCB6-4A95-9064-BB0BF8C0716D");
    public const uint QuestPurchase3EnergyDrinksReward = 10_000;
    
    public static readonly Guid QuestIdPurchase5EnergyDrinks = Guid.Parse("ED555555-FCB6-4A95-9064-BB0BF8C0716D");
    public const uint QuestPurchase5EnergyDrinksReward = 20_000;
}