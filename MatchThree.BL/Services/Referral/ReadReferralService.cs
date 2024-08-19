using MatchThree.Domain.Interfaces.Referral;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using Microsoft.EntityFrameworkCore;

namespace MatchThree.BL.Services.Referral;

public class ReadReferralService (MatchThreeDbContext context)
    : IReadReferralService
{
    public async Task<long?> ReferrerIdByReferralIdAsync(long referralId)
    {
        var dbModel = await context.Set<ReferralDbModel>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ReferralUserId == referralId);

        return dbModel?.ReferrerUserId;
    }
}