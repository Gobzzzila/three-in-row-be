using MatchThree.Shared.Enums;

namespace MatchThree.Domain.Interfaces.Upgrades;

public interface IUpgradesRestrictionsService
{
    Task<bool> ValidateEnergyReserveRestrictions(long userId, int requiredReferralsAmount);
    
    bool ValidateEnergyRecoveryRestrictions(EnergyReserveLevels currentLevel, EnergyReserveLevels restrictedLevel);
}