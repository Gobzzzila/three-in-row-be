namespace MatchThree.Domain.Interfaces.Referral;

public interface IDeleteReferralService
{
    Task DeleteByUserIdAsync(long userId);
}