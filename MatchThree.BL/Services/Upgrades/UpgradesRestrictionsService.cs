using MatchThree.Domain.Interfaces;
using MatchThree.Domain.Interfaces.Referral;
using MatchThree.Domain.Interfaces.Upgrades;
using MatchThree.Shared.Enums;

namespace MatchThree.BL.Services.Upgrades;

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