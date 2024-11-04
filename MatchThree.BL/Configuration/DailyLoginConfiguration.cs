using MatchThree.Shared.Constants;

namespace MatchThree.BL.Configuration;

public static class DailyLoginConfiguration
{
    public static readonly List<int> DailyRewards =
    [
        DailyLoginConstants.DailyQuestDay1Reward, 
        DailyLoginConstants.DailyQuestDay2Reward,
        DailyLoginConstants.DailyQuestDay3Reward, 
        DailyLoginConstants.DailyQuestDay4Reward,
        DailyLoginConstants.DailyQuestDay5Reward,
        DailyLoginConstants.DailyQuestDay6Reward,
        DailyLoginConstants.DailyQuestDay7Reward,
        DailyLoginConstants.DailyQuestDay8Reward,
        DailyLoginConstants.DailyQuestDay9Reward,
        DailyLoginConstants.DailyQuestDay10Reward,
        DailyLoginConstants.DailyQuestDay11Reward,
        DailyLoginConstants.DailyQuestDay12Reward,
        DailyLoginConstants.DailyQuestDay13Reward,
        DailyLoginConstants.DailyQuestDay14Reward
    ];

    public static int ShortenIndex(int streakCount)
    {
        return streakCount <= DailyRewards.Count - 1 
            ? streakCount 
            : DailyRewards.Count - 1;
    }
}