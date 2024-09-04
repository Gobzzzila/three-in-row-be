using MatchThree.Domain.Models;

namespace MatchThree.Domain.Interfaces.Referral;

public interface IReadReferralService
{
    Task<IReadOnlyCollection<ReferralEntity>> GetReferralsByReferrerId(long referrerId);
    Task<long?> GetReferrerIdByReferralIdAsync(long referralId);
    Task<int> GetReferralAmountByReferrerIdAsync(long referrerId);
}