using MatchThree.BL.Configuration;
using MatchThree.Domain.Interfaces.Energy;
using MatchThree.Domain.Interfaces.Upgrades;
using MatchThree.Domain.Models;
using MatchThree.Shared.Constants;
using MatchThree.Shared.Enums;

namespace MatchThree.BL.Services.Upgrades;

public class GetUpgradesService(IReadEnergyService readEnergyService) : IGetUpgradesService
{
    private readonly IReadEnergyService _readEnergyService = readEnergyService;

    public async Task<IReadOnlyCollection<UpgradeEntity>> GetAll(long userId)
    {
        var energyEntity = await _readEnergyService.GetByUserIdAsync(userId);

        var result = new List<UpgradeEntity>
        {
            GetEnergyReserveUpgrade(energyEntity),
            GetEnergyRecoveryUpgrade(energyEntity),
        };

        return result;
    }

    private UpgradeEntity GetEnergyReserveUpgrade(EnergyEntity energyEntity)
    {
        var reserveParams = EnergyReserveConfiguration.GetParamsByLevel(energyEntity.MaxReserve);

        var upgradeEntity = new UpgradeEntity
        {
            HeaderTextKey = TranslationConstants.UpgradeEnergyReserveHeaderKey,
            DescriptionTextKey = TranslationConstants.UpgradeEnergyReserveDescriptionKey,
            BlockingTextKey = TranslationConstants.UpgradeEnergyReserveBlockingTextKey,
            CurrentLevel = (int)energyEntity.MaxReserve,
            Price = reserveParams.NextLevelCost,
            IsStars = false,
            Category = UpgradeCategories.Energy,
            ExecutePathName = EndpointsConstants.UpgradeEnergyReserveEndpointName
        };
        return upgradeEntity;
    }
    
    private UpgradeEntity GetEnergyRecoveryUpgrade(EnergyEntity energyEntity)
    {
        var recoveryParams = EnergyRecoveryConfiguration.GetParamsByLevel(energyEntity.RecoveryLevel);

        var upgradeEntity = new UpgradeEntity
        {
            HeaderTextKey = TranslationConstants.UpgradeEnergyRecoveryHeaderKey,
            DescriptionTextKey = TranslationConstants.UpgradeEnergyRecoveryDescriptionKey,
            BlockingTextKey = TranslationConstants.UpgradeEnergyRecoveryBlockingTextKey,
            CurrentLevel = (int)energyEntity.RecoveryLevel,
            Price = recoveryParams.NextLevelCost,
            IsStars = false,
            Category = UpgradeCategories.Energy,
            ExecutePathName = EndpointsConstants.UpgradeEnergyRecoveryEndpointName
        };
        return upgradeEntity;
    }
}