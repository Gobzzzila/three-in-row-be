using AutoMapper;
using MatchThree.Domain.Interfaces.LeaderboardMember;
using MatchThree.Domain.Models;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using MatchThree.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace MatchThree.BL.Services.LeaderboardMember;

public class ReadLeaderboardMemberService(MatchThreeDbContext context, 
    IMapper mapper) 
    : IReadLeaderboardMemberService
{
    public async Task<IReadOnlyCollection<LeaderboardMemberEntity>> GetLeaderboardByLeagueAsync(LeagueTypes league)
    {
        var dbModels = await context.Set<LeaderboardMemberDbModel>()
            .AsNoTracking()
            .Where(x => x.League == league)
            .ToListAsync();

        return mapper.Map<IReadOnlyCollection<LeaderboardMemberEntity>>(dbModels);
    }
    
    public async Task<ushort> GetTopSpotByUserId(long userId)
    {
        var dbModel = await context.Set<LeaderboardMemberDbModel>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == userId);

        return dbModel?.TopSpot ?? 1001;
    }
}