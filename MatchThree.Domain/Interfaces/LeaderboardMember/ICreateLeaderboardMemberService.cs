using MatchThree.Shared.Enums;

namespace MatchThree.Domain.Interfaces.LeaderboardMember;

public interface ICreateLeaderboardMemberService
{
    Task CreateByLeagueTypeAsync(LeagueTypes league);
}