using MatchThree.Domain.Interfaces.Referral;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using Microsoft.EntityFrameworkCore;

namespace MatchThree.BL.Services.Referral;

public class ReadReferralService (MatchThreeDbContext context)
    : IReadReferralService
{
    private readonly MatchThreeDbContext _context = context;

    public async Task<long?> ReferrerIdByReferralIdAsync(long referralId)
    {
        var dbModel = await _context.Set<ReferralDbModel>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ReferralUserId == referralId);

        return dbModel?.ReferrerUserId;
    }
    
    public async Task<int> ReferralAmountByReferrerIdAsync(long referrerId)
    {
        var referralsAmount = await _context.Set<ReferralDbModel>()
            .AsNoTracking()
            .Where(x => x.ReferrerUserId == referrerId)
            .CountAsync();

        return referralsAmount;
    }
}