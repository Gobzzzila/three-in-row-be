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
                    Reward = QuestsConstants.QuestInvite1FriendReward,
                    ExternalLinkKey = null,
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
                    Reward = QuestsConstants.QuestInvite3FriendReward,
                    ExternalLinkKey = null,
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
                    Reward = QuestsConstants.QuestInvite5FriendReward,
                    ExternalLinkKey = null,
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
                    Reward = QuestsConstants.QuestPingwinNewsChannelSubscriptionReward,
                    ExternalLinkKey = TranslationConstants.LinkPingwinNewsChannelTextKey,
                    SecretCode = null,
                    VerificationOfFulfillment = IsSubscribedToChannel(ChatConstants.PingwinNewsChannelId, ChatConstants.PingwinNewsChannelIdRu),
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
                    Reward = QuestsConstants.QuestPingwinGroupChatSubscriptionReward,
                    ExternalLinkKey = TranslationConstants.LinkGroupChatTextKey,
                    SecretCode = null,
                    VerificationOfFulfillment = IsSubscribedToChannel(ChatConstants.PingwinGroupChatId, ChatConstants.PingwinGroupChatIdRu),
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
    
    private static Func<IValidateQuestCompletionService, long, Task<bool>> IsSubscribedToChannel(params long[] chatIds)
    {
        return (questCompletionService, userId) => questCompletionService.IsSubscribedToChannelAsync(userId, chatIds);
    }
}