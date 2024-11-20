using MatchThree.Shared.Enums;

namespace MatchThree.Domain.Interfaces.LeaderboardMember;

public interface IDeleteLeaderboardMemberService
{
    Task DeleteByLeagueTypeAsync(LeagueTypes league);
    
    Task DeleteAll();
}