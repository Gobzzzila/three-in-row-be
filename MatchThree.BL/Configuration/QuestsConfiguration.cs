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
        return (from quest in Quests 
            where !completedQuestIds.Contains(quest.Key) 
            select quest.Value).ToList();
    }
    
    public static List<QuestEntity> GetCompleted(List<Guid> completedQuestIds)
    {
        return (from quest in Quests 
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
                    VerificationOfFulfillment = IsEnoughReferralsAsync(1)
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
                    VerificationOfFulfillment = IsEnoughReferralsAsync(3)
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
                    VerificationOfFulfillment = IsEnoughReferralsAsync(5)
                }
            },
            {
                QuestsConstants.QuestIdNewsChannelSubscription, new QuestEntity
                {
                    Id = QuestsConstants.QuestIdNewsChannelSubscription,
                    Type = QuestTypes.TelegramChannel,
                    TittleKey = TranslationConstants.QuestSubscribeToNewsChannelHeaderTextKey,
                    DescriptionKey = TranslationConstants.QuestSubscribeToNewsChannelDescriptionTextKey,
                    Reward = QuestsConstants.QuestNewsChannelSubscriptionReward,
                    ExternalLinkKey = ChatConstants.LinkNewsChannelTextKey,
                    SecretCode = null,
                    VerificationOfFulfillment = IsSubscribedToChannel(ChatConstants.NewsChannelId, ChatConstants.NewsChannelIdRu)
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