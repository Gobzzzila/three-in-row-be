using System.Collections.Frozen;
using MatchThree.Domain.Interfaces.Quests;
using MatchThree.Domain.Models;
using MatchThree.Shared.Constants;
using MatchThree.Shared.Enums;

namespace MatchThree.BL.Configuration;

public static class QuestsConfiguration
{
    private static readonly FrozenDictionary<Guid, QuestEntity> Quests;

    public static QuestEntity GetById(Guid questId)
    {
        return Quests[questId];
    }
    
    public static List<QuestEntity> GetUncompleted(List<Guid> completedQuestIds)
    {
        var uncompletedQuest = (from quest in Quests
            where !completedQuestIds.Contains(quest.Key) && !quest.Value.IsDeleted
            select quest.Value).ToList();

        var compressibleQuests = uncompletedQuest.OrderBy(x => x.Reward)
            .Where(x => x.IsСompressible)
            .DistinctBy(x => x.Type)
            .ToList(); 
        
        var nonCompressibleQuests = uncompletedQuest
            .Where(x => !x.IsСompressible)
            .ToList(); 
        
        return compressibleQuests.Concat(nonCompressibleQuests).OrderBy(x => x.Type).ToList();
    }
    
    public static List<QuestEntity> GetCompleted(List<Guid> completedQuestIds)
    {
        return (from quest in Quests.OrderBy(x => x.Value.Type)
            where completedQuestIds.Contains(quest.Key) 
            select quest.Value).ToList();
    }
    
    static QuestsConfiguration()
    {
        var dictionary = new Dictionary<Guid, QuestEntity>
        {
            {
                QuestsConstants.QuestIdInvite1Friend, new QuestEntity
                {
                    Id = QuestsConstants.QuestIdInvite1Friend,
                    Type = QuestTypes.InvitingFriends,
                    TittleKey = TranslationConstants.QuestInvite1FriendTittleTextKey,
                    DescriptionKey = TranslationConstants.QuestInvite1FriendDescriptionTextKey,
                    PictureName = QuestsConstants.QuestInviteFriendPictureName,
                    Reward = QuestsConstants.QuestInvite1FriendReward,
                    ActionLinkKey = null,
                    IsLinkInternal = null,
                    SecretCode = null,
                    VerificationOfFulfillment = IsEnoughReferralsAsync(1),
                    IsDeleted = false,
                    IsСompressible = true
                }
            },
            {
                QuestsConstants.QuestIdInvite3Friend, new QuestEntity
                {
                    Id = QuestsConstants.QuestIdInvite3Friend,
                    Type = QuestTypes.InvitingFriends,
                    TittleKey = TranslationConstants.QuestInvite3FriendTittleTextKey,
                    DescriptionKey = TranslationConstants.QuestInvite3FriendDescriptionTextKey,
                    PictureName = QuestsConstants.QuestInviteFriendPictureName,
                    Reward = QuestsConstants.QuestInvite3FriendReward,
                    ActionLinkKey = null,
                    IsLinkInternal = null,
                    SecretCode = null,
                    VerificationOfFulfillment = IsEnoughReferralsAsync(3),
                    IsDeleted = false,
                    IsСompressible = true
                }
            },
            {
                QuestsConstants.QuestIdInvite5Friend, new QuestEntity
                {
                    Id = QuestsConstants.QuestIdInvite5Friend,
                    Type = QuestTypes.InvitingFriends,
                    TittleKey = TranslationConstants.QuestInvite5FriendTittleTextKey,
                    DescriptionKey = TranslationConstants.QuestInvite5FriendDescriptionTextKey,
                    PictureName = QuestsConstants.QuestInviteFriendPictureName,
                    Reward = QuestsConstants.QuestInvite5FriendReward,
                    ActionLinkKey = null,
                    IsLinkInternal = null,
                    SecretCode = null,
                    VerificationOfFulfillment = IsEnoughReferralsAsync(5),
                    IsDeleted = false,
                    IsСompressible = true
                }
            },
            {
                QuestsConstants.QuestIdPingwinNewsChannelSubscription, new QuestEntity
                {
                    Id = QuestsConstants.QuestIdPingwinNewsChannelSubscription,
                    Type = QuestTypes.TelegramChannel,
                    TittleKey = TranslationConstants.QuestSubscribeToNewsChannelTittleTextKey,
                    DescriptionKey = TranslationConstants.QuestSubscribeToNewsChannelDescriptionTextKey,
                    PictureName = QuestsConstants.QuestTgSubscriptionPictureName,
                    Reward = QuestsConstants.QuestPingwinNewsChannelSubscriptionReward,
                    ActionLinkKey = TranslationConstants.LinkPingwinNewsChannelTextKey,
                    IsLinkInternal = true,
                    SecretCode = null,
                    VerificationOfFulfillment = IsSubscribedToChannelAsync(ChatConstants.PingwinNewsChannelIdEn, ChatConstants.PingwinNewsChannelIdRu),
                    IsDeleted = false,
                    IsСompressible = false
                }
            },
            {
                QuestsConstants.QuestIdPingwinGroupChatSubscription, new QuestEntity
                {
                    Id = QuestsConstants.QuestIdPingwinGroupChatSubscription,
                    Type = QuestTypes.TelegramChannel,
                    TittleKey = TranslationConstants.QuestSubscribeToGroupChatTittleTextKey,
                    DescriptionKey = TranslationConstants.QuestSubscribeToGroupChatDescriptionTextKey,
                    PictureName = QuestsConstants.QuestTgSubscriptionPictureName,
                    Reward = QuestsConstants.QuestPingwinGroupChatSubscriptionReward,
                    ActionLinkKey = TranslationConstants.LinkGroupChatTextKey,
                    IsLinkInternal = true,
                    SecretCode = null,
                    VerificationOfFulfillment = IsSubscribedToChannelAsync(ChatConstants.PingwinGroupChatIdEn, ChatConstants.PingwinGroupChatIdRu),
                    IsDeleted = false,
                    IsСompressible = false
                }
            },
            {
                QuestsConstants.QuestIdWelcomeMysteryCode, new QuestEntity
                {
                    Id = QuestsConstants.QuestIdWelcomeMysteryCode,
                    Type = QuestTypes.MysteryCode,
                    TittleKey = TranslationConstants.QuestWelcomeMysteryCodeTittleTextKey,
                    DescriptionKey = TranslationConstants.QuestWelcomeMysteryCodeDescriptionTextKey,
                    PictureName = QuestsConstants.QuestMysteryCodePictureName,
                    Reward = QuestsConstants.QuestWelcomeMysteryCodeReward,
                    ActionLinkKey = TranslationConstants.LinkPingwinNewsChannelTextKey,
                    IsLinkInternal = true,
                    SecretCode = QuestsConstants.QuestWelcomeMysteryCodeSecretCode,
                    VerificationOfFulfillment = null,
                    IsDeleted = false,
                    IsСompressible = false
                }
            },
            {
                QuestsConstants.QuestIdPurchase1EnergyDrink, new QuestEntity
                {
                    Id = QuestsConstants.QuestIdPurchase1EnergyDrink,
                    Type = QuestTypes.EnergyDrinkPurchaser,
                    TittleKey = TranslationConstants.QuestPurchase1EnergyDrinkTittleTextKey,
                    DescriptionKey = TranslationConstants.QuestPurchaseEnergyDrinkDescriptionTextKey,
                    PictureName = QuestsConstants.QuestEnergyDrinkPurchaserPictureName,
                    Reward = QuestsConstants.QuestPurchase1EnergyDrinkReward,
                    ActionLinkKey = null,
                    IsLinkInternal = null,
                    SecretCode = null,
                    VerificationOfFulfillment = IsEnoughBoughtEnergyDrinksAsync(1),
                    IsDeleted = false,
                    IsСompressible = true
                }
            },
            {
                QuestsConstants.QuestIdPurchase3EnergyDrinks, new QuestEntity
                {
                    Id = QuestsConstants.QuestIdPurchase3EnergyDrinks,
                    Type = QuestTypes.EnergyDrinkPurchaser,
                    TittleKey = TranslationConstants.QuestPurchase3EnergyDrinkTittleTextKey,
                    DescriptionKey = TranslationConstants.QuestPurchaseEnergyDrinkDescriptionTextKey,
                    PictureName = QuestsConstants.QuestEnergyDrinkPurchaserPictureName,
                    Reward = QuestsConstants.QuestPurchase3EnergyDrinksReward,
                    ActionLinkKey = null,
                    IsLinkInternal = null,
                    SecretCode = null,
                    VerificationOfFulfillment = IsEnoughBoughtEnergyDrinksAsync(3),
                    IsDeleted = false,
                    IsСompressible = true
                }
            },
            {
                QuestsConstants.QuestIdPurchase5EnergyDrinks, new QuestEntity
                {
                    Id = QuestsConstants.QuestIdPurchase5EnergyDrinks,
                    Type = QuestTypes.EnergyDrinkPurchaser,
                    TittleKey = TranslationConstants.QuestPurchase5EnergyDrinkTittleTextKey,
                    DescriptionKey = TranslationConstants.QuestPurchaseEnergyDrinkDescriptionTextKey,
                    PictureName = QuestsConstants.QuestEnergyDrinkPurchaserPictureName,
                    Reward = QuestsConstants.QuestPurchase5EnergyDrinksReward,
                    ActionLinkKey = null,
                    IsLinkInternal = null,
                    SecretCode = null,
                    VerificationOfFulfillment = IsEnoughBoughtEnergyDrinksAsync(5),
                    IsDeleted = false,
                    IsСompressible = true
                }
            },
            {
                QuestsConstants.QuestIdBePremiumUser, new QuestEntity
                {
                    Id = QuestsConstants.QuestIdBePremiumUser,
                    Type = QuestTypes.PremiumUser,
                    TittleKey = TranslationConstants.QuestBePremiumUserTittleTextKey,
                    DescriptionKey = TranslationConstants.QuestBePremiumUserDescriptionTextKey,
                    PictureName = QuestsConstants.QuestBePremiumUserPictureName,
                    Reward = QuestsConstants.QuestBePremiumUserReward,
                    ActionLinkKey = null,
                    IsLinkInternal = null,
                    SecretCode = null,
                    VerificationOfFulfillment = IsPremiumUserAsync(),
                    IsDeleted = false,
                    IsСompressible = false
                }
            },
        };

        Quests = dictionary.ToFrozenDictionary();
    }
    
    private static Func<IValidateQuestCompletionService, long, Task<bool>> IsEnoughReferralsAsync(int requiredReferralsAmount)
    {
        return (questCompletionService, userId) => 
            questCompletionService.IsEnoughReferralsAsync(userId, requiredReferralsAmount);
    }
    
    private static Func<IValidateQuestCompletionService, long, Task<bool>> IsSubscribedToChannelAsync(params long[] chatIds)
    {
        return (questCompletionService, userId) => questCompletionService.IsSubscribedToChannelAsync(userId, chatIds);
    }
    
    private static Func<IValidateQuestCompletionService, long, Task<bool>> IsEnoughBoughtEnergyDrinksAsync(int requiredEnergyDrinksAmount)
    {
        return (questCompletionService, userId) => questCompletionService.IsEnoughBoughtEnergyDrinksAsync(userId, requiredEnergyDrinksAmount);
    }
    
    private static Func<IValidateQuestCompletionService, long, Task<bool>> IsPremiumUserAsync()
    {
        return (questCompletionService, userId) => questCompletionService.IsPremiumUserAsync(userId);
    }
}