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
    public const string QuestTgSubscriptionPictureName = "tg_icon.png";

    public static readonly Guid QuestIdPingwinNewsChannelSubscription = Guid.Parse("FCA11111-908A-4619-8868-F8310EA7D2E3");
    public const uint QuestPingwinNewsChannelSubscriptionReward = 2_500;
    
    public static readonly Guid QuestIdPingwinGroupChatSubscription = Guid.Parse("FCA22222-908A-4619-8868-F8310EA7D2E3");
    public const uint QuestPingwinGroupChatSubscriptionReward = 2_500;
}