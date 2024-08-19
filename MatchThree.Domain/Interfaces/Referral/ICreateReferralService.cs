namespace MatchThree.Domain.Interfaces.Referral;

public interface ICreateReferralService
{
    Task CreateAsync(long referrerId, long referralId, bool isPremium);
}