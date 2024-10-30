using MatchThree.BL.Configuration;
using MatchThree.Domain.Interfaces.Referral;
using MatchThree.Domain.Models;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using Microsoft.EntityFrameworkCore;

namespace MatchThree.BL.Services.Referral;

public class ReadReferralService (MatchThreeDbContext context)
    : IReadReferralService
{
    private readonly MatchThreeDbContext _context = context;

    public async Task<IReadOnlyCollection<ReferralEntity>> GetReferralsByReferrerId(long referrerId)
    {
        var entities = await _context.Set<ReferralDbModel>()
            .AsNoTracking()
            .Include(x => x.Referral)
            .ThenInclude(x => x!.Balance)
            .Where(x => x.ReferrerUserId == referrerId)
            .Take(100)
            .Select(x => new ReferralEntity
            {
                Id = x.ReferralUserId,
                WasPremium = x.WasPremium,
                FirstName = x.Referral!.FirstName,
                OverallBalance = x!.Referral!.Balance!.OverallBalance
            })
            .ToListAsync();

        foreach (var entity in entities)
        {
            entity.League = LeagueConfiguration.CalculateLeague(entity.OverallBalance);
            entity.Brought = LeagueConfiguration.CalculateReferralProfit(entity.League, entity.WasPremium);
        }

        return entities;
    }
    
    public async Task<long?> GetReferrerIdByReferralIdAsync(long referralId)
    {
        var dbModel = await _context.Set<ReferralDbModel>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ReferralUserId == referralId);

        return dbModel?.ReferrerUserId;
    }
    
    public async Task<int> GetReferralAmountByReferrerIdAsync(long referrerId)
    {
        var referralsAmount = await _context.Set<ReferralDbModel>()
            .AsNoTracking()
            .Where(x => x.ReferrerUserId == referrerId)
            .CountAsync();

        return referralsAmount;
    }
}