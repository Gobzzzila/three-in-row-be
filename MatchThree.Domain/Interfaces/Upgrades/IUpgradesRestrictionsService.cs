using MatchThree.Shared.Enums;

namespace MatchThree.Domain.Interfaces.Upgrades;

public interface IUpgradesRestrictionsService
{
    Task<int?> CalculateNumberOfMissingReferralsAsync(long userId, int requiredReferralsAmount);
    
    int? ValidateEnergyReserveLevel(EnergyReserveLevels currentLevel, EnergyReserveLevels restrictedReserveLevel);
    
    Task<int?> ValidateFieldLevel(long userId, FieldLevels restrictedFieldLevel);
}