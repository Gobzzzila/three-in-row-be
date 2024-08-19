using MatchThree.Domain.Interfaces.Balance;
using MatchThree.Domain.Interfaces.Referral;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using MatchThree.Shared.Constants;

namespace MatchThree.BL.Services.Referral;

public class CreateReferralService (MatchThreeDbContext context, 
    IUpdateBalanceService updateBalanceService)
    : ICreateReferralService
{
    public async Task CreateAsync(long referrerId, long referralId, bool isPremium)
    {
        await context.Set<ReferralDbModel>().AddAsync(new ReferralDbModel
        {
            Id = Guid.NewGuid(),
            ReferralUserId = referralId,
            ReferrerUserId = referrerId,
            WasPremium = isPremium
        });

        var referrerReward = isPremium
            ? ReferralConstants.RewardForInvitePremiumUser
            : ReferralConstants.RewardForInviteRegularUser;
        await updateBalanceService.AddBalanceAsync(referrerId, referrerReward);
    }
}