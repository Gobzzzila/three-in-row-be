using MatchThree.Domain.Interfaces.Quests;
using MatchThree.Domain.Models;
using MatchThree.Shared.Constants;
using MatchThree.Shared.Enums;

namespace MatchThree.BL.Configuration;

public static class QuestsConfiguration
{
    private static readonly Dictionary<Guid, QuestEntity> Quests;

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
        Quests = new Dictionary<Guid, QuestEntity>
        {
            {
                Guid.Parse("F1111111-908A-4619-8868-F8310EA7D2E3"), new QuestEntity
                {
                    Id = Guid.Parse("F1111111-908A-4619-8868-F8310EA7D2E3"),
                    Type = QuestTypes.InvitingFriends,
                    TittleKey = QuestsConstants.Invite1FriendTittleTextKey,
                    DescriptionKey = null,
                    Reward = QuestsConstants.Invite1FriendReward,
                    ExecuteLink = null,
                    SecretCode = null,
                    VerificationOfFulfillment = IsEnoughReferralsAsync(1)
                }
            }
        };
    }
    
    private static Func<IQuestCompletionService, long, Task<bool>> IsEnoughReferralsAsync(int requiredReferralsAmount)
    {
        return (questCompletionService, userId) => 
            questCompletionService.IsEnoughReferralsAsync(userId, requiredReferralsAmount);
    }
}