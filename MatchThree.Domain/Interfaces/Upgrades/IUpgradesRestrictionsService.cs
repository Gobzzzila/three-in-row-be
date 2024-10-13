using MatchThree.Shared.Enums;

namespace MatchThree.Domain.Interfaces.Upgrades;

public interface IUpgradesRestrictionsService
{
    Task<int?> CalculateNumberOfMissingReferralsAsync(long userId, int requiredReferralsAmount);
    
    int? ValidateEnergyReserveLevel(EnergyReserveLevels currentLevel, EnergyReserveLevels restrictedLevel);
}