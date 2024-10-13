using MatchThree.Domain.Interfaces.Referral;
using MatchThree.Domain.Interfaces.Upgrades;
using MatchThree.Shared.Enums;

namespace MatchThree.BL.Services.Upgrades;

public class UpgradesRestrictionsService(IReadReferralService readReferralService) : IUpgradesRestrictionsService
{
    private readonly IReadReferralService _readReferralService = readReferralService;

    public async Task<int?> CalculateNumberOfMissingReferralsAsync(long userId, int requiredReferralsAmount)
    {
        var referralsAmount = await _readReferralService.GetReferralAmountByReferrerIdAsync(userId);
        if (referralsAmount >= requiredReferralsAmount) 
            return default;
        
        return requiredReferralsAmount - referralsAmount;
    }

    public int? ValidateEnergyReserveLevel(EnergyReserveLevels currentLevel, EnergyReserveLevels restrictedLevel)
    {
        if (currentLevel >= restrictedLevel) 
            return null;
        
        return (int)restrictedLevel;
    }
}