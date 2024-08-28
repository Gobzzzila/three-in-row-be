namespace MatchThree.Domain.Interfaces.Referral;

public interface IReadReferralService
{
    Task<long?> ReferrerIdByReferralIdAsync(long referralId);
    Task<int> ReferralAmountByReferrerIdAsync(long referrerId);
}