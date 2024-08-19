using MatchThree.Domain.Interfaces.Referral;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using Microsoft.EntityFrameworkCore;

namespace MatchThree.BL.Services.Referral;

public class DeleteReferralService (MatchThreeDbContext context) 
    : IDeleteReferralService
{
    public Task DeleteByUserIdAsync(long userId)
    {
        return context.Set<ReferralDbModel>()
            .Where(x => x.ReferralUserId == userId || x.ReferrerUserId == userId)
            .ExecuteDeleteAsync();
    }
}