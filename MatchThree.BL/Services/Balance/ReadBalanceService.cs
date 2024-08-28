using AutoMapper;
using MatchThree.Domain.Interfaces.Balance;
using MatchThree.Domain.Interfaces.LeaderboardMember;
using MatchThree.Domain.Models;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using MatchThree.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace MatchThree.BL.Services.Balance;

public class ReadBalanceService(MatchThreeDbContext context, 
    IReadLeaderboardMemberService readLeaderboardMemberService,
    IMapper mapper) : IReadBalanceService
{
    private readonly MatchThreeDbContext _context = context;
    private readonly IReadLeaderboardMemberService _readLeaderboardMemberService = readLeaderboardMemberService;
    private readonly IMapper _mapper = mapper;

    public async Task<BalanceEntity> GetByIdAsync(long id)
    {
        var dbModel = await _context.Set<BalanceDbModel>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
        if (dbModel is null)
            throw new NoDataFoundException();

        var entity = _mapper.Map<BalanceEntity>(dbModel);
        entity.TopSpot = await _readLeaderboardMemberService.GetTopSpotByUserId(entity.Id);

        return entity;
    }
}