using MatchThree.Domain.Interfaces;
using MatchThree.Domain.Interfaces.Referral;
using MatchThree.Shared.Enums;

namespace MatchThree.BL.Services;

public class UpgradesRestrictionsService(IReadReferralService readReferralService) : IUpgradesRestrictionsService
{
    private readonly IReadReferralService _readReferralService = readReferralService;

    public async Task<bool> ValidateEnergyReserveRestrictions(long userId, int requiredReferralsAmount)
    {
        var referralAmount = await _readReferralService.GetReferralAmountByReferrerIdAsync(userId);
        return referralAmount >= requiredReferralsAmount;
    }

    public bool ValidateEnergyRecoveryRestrictions(EnergyReserveLevels currentLevel, EnergyReserveLevels restrictedLevel)
    {
        return currentLevel >= restrictedLevel;
    }
}