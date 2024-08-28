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
    private readonly MatchThreeDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<IReadOnlyCollection<LeaderboardMemberEntity>> GetLeaderboardByLeagueAsync(LeagueTypes league)
    {
        var dbModels = await _context.Set<LeaderboardMemberDbModel>()
            .AsNoTracking()
            .Where(x => x.League == league)
            .ToListAsync();

        return _mapper.Map<IReadOnlyCollection<LeaderboardMemberEntity>>(dbModels);
    }
    
    public async Task<int> GetTopSpotByUserId(long userId)
    {
        var dbModel = await _context.Set<LeaderboardMemberDbModel>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == userId);

        return dbModel?.TopSpot ?? 1001;
    }
}