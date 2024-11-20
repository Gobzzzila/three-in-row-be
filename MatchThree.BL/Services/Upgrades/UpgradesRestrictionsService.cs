using MatchThree.Domain.Interfaces.Field;
using MatchThree.Domain.Interfaces.Referral;
using MatchThree.Domain.Interfaces.Upgrades;
using MatchThree.Shared.Enums;

namespace MatchThree.BL.Services.Upgrades;

public class UpgradesRestrictionsService(IReadReferralService readReferralService, 
    IReadFieldService readFieldService) 
    : IUpgradesRestrictionsService
{
    private readonly IReadReferralService _readReferralService = readReferralService;
    private readonly IReadFieldService _readFieldService = readFieldService;

    public async Task<int?> CalculateNumberOfMissingReferralsAsync(long userId, int requiredReferralsAmount)
    {
        var referralsAmount = await _readReferralService.GetReferralAmountByReferrerIdAsync(userId);
        if (referralsAmount >= requiredReferralsAmount) 
            return default;
        
        return requiredReferralsAmount - referralsAmount;
    }

    public int? ValidateEnergyReserveLevel(EnergyReserveLevels currentLevel, EnergyReserveLevels restrictedReserveLevel)
    {
        if (currentLevel >= restrictedReserveLevel) 
            return default;
        
        return (int)restrictedReserveLevel;
    }
    
    public async Task<int?> ValidateFieldLevel(long userId, FieldLevels restrictedFieldLevel)
    {
        var fieldEntity = await _readFieldService.GetByUserIdAsync(userId);
        if (fieldEntity.FieldLevel >= restrictedFieldLevel) 
            return default;
        
        return (int)restrictedFieldLevel;
    }
}