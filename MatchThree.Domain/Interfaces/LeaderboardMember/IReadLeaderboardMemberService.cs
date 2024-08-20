using MatchThree.Domain.Models;
using MatchThree.Shared.Enums;

namespace MatchThree.Domain.Interfaces.LeaderboardMember;

public interface IReadLeaderboardMemberService
{
    Task<IReadOnlyCollection<LeaderboardMemberEntity>> GetLeaderboardByLeagueAsync(LeagueTypes league);
    Task<int> GetTopSpotByUserId(long userId);
}