using MatchThree.Domain.Models.Leaderboard;
using MatchThree.Shared.Enums;

namespace MatchThree.Domain.Interfaces.LeaderboardMember;

public interface IReadLeaderboardMemberService
{
    Task<LeaderboardEntity> GetLeaderboardByLeagueAsync(LeagueTypes league);
    Task<int> GetTopSpotByUserId(long userId);
}