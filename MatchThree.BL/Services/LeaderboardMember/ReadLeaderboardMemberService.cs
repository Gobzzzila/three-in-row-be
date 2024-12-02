using AutoMapper;
using MatchThree.BL.Configuration;
using MatchThree.Domain.Interfaces.LeaderboardMember;
using MatchThree.Domain.Models.Leaderboard;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using MatchThree.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace MatchThree.BL.Services.LeaderboardMember;

public class ReadLeaderboardMemberService(MatchThreeDbContext context, 
    IMapper mapper) 
    : IReadLeaderboardMemberService
{
    private readonly MatchThreeDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<LeaderboardEntity> GetLeaderboardByLeagueAsync(LeagueTypes league)
    {
        var dbModels = await _context.Set<LeaderboardMemberDbModel>()
            .AsNoTracking()
            .Where(x => x.League == league)
            .Take(100)
            .OrderBy(x => x.TopSpot)
            .ToListAsync();

        var leagueParams = LeagueConfiguration.GetParamsByType(league);
        return new LeaderboardEntity
        {
            League = league,
            LeagueFullNameTextKey = leagueParams.LeagueFullNameTextKey,
            MinValue = leagueParams.MinValue,
            MaxValue = leagueParams.MaxValue,
            NextLeague = leagueParams.NextLeague,
            PreviousLeague = leagueParams.PreviousLeague,
            Members = _mapper.Map<List<LeaderboardMemberEntity>>(dbModels)
        };
    }
    
    public async Task<int> GetTopSpotAsync(long userId, LeagueTypes league)
    {
        var dbModel = await _context.Set<LeaderboardMemberDbModel>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.League == league && x.Id == userId);

        return dbModel?.TopSpot ?? 1000;
    }
}