using MatchThree.BL.Configuration;
using MatchThree.Domain.Interfaces.Energy;
using MatchThree.Domain.Interfaces.Upgrades;
using MatchThree.Domain.Models;
using MatchThree.Domain.Models.Upgrades;
using MatchThree.Shared.Constants;
using MatchThree.Shared.Enums;

namespace MatchThree.BL.Services.Upgrades;

public class GetUpgradesService(IUpgradesRestrictionsService upgradesRestrictionsService, 
    IReadEnergyService readEnergyService) 
    : IGetUpgradesService
{
    private readonly IUpgradesRestrictionsService _upgradesRestrictionsService = upgradesRestrictionsService;
    private readonly IReadEnergyService _readEnergyService = readEnergyService;

    public async Task<IReadOnlyCollection<GroupedUpgradesEntity>> GetAll(long userId)
    {
        var energyEntity = await _readEnergyService.GetByUserIdAsync(userId);

        var result = new List<GroupedUpgradesEntity>
        {
            await GetEnergyUpgrades(energyEntity)
        };
        
        return result;
    }

    private async ValueTask<GroupedUpgradesEntity> GetEnergyUpgrades(EnergyEntity energyEntity)
    {
        var result = new GroupedUpgradesEntity
        {
            Category = UpgradeCategories.Energy,
            Upgrades =
            [
                await GetEnergyReserveUpgrade(energyEntity),
                GetEnergyRecoveryUpgrade(energyEntity)
            ]
        };

        return result;
    }

    private async ValueTask<UpgradeEntity> GetEnergyReserveUpgrade(EnergyEntity energyEntity)
    {
        var reserveParams = EnergyReserveConfiguration.GetParamsByLevel(energyEntity.MaxReserve);
        
        int? missingAmountOfReferrals = default;
        if (reserveParams.UpgradeCondition is not null)
        {
            missingAmountOfReferrals = 
                await reserveParams.UpgradeCondition(_upgradesRestrictionsService, energyEntity.Id);
        }
        
        var upgradeEntity = new UpgradeEntity
        {
            HeaderTextKey = TranslationConstants.UpgradeEnergyReserveHeaderKey,
            DescriptionTextKey = TranslationConstants.UpgradeEnergyReserveDescriptionKey,
            BlockingTextKey = missingAmountOfReferrals is not null
                ? TranslationConstants.UpgradeEnergyReserveBlockingTextKey
                : null,
            BlockingTextArgs = [missingAmountOfReferrals],
            CurrentLevel = (int)energyEntity.MaxReserve,
            Price = reserveParams.NextLevelCost,
            IsStars = false,
            ExecutePathName = EndpointsConstants.UpgradeEnergyReserveEndpointName
        };
        return upgradeEntity;
    }
    
    private UpgradeEntity GetEnergyRecoveryUpgrade(EnergyEntity energyEntity)
    {
        var recoveryParams = EnergyRecoveryConfiguration.GetParamsByLevel(energyEntity.RecoveryLevel);

        int? requiredReserveLevel = default;
        if (recoveryParams.UpgradeCondition is not null)
        {
            requiredReserveLevel = recoveryParams.UpgradeCondition(_upgradesRestrictionsService, energyEntity.MaxReserve);
        }
        
        var upgradeEntity = new UpgradeEntity
        {
            HeaderTextKey = TranslationConstants.UpgradeEnergyRecoveryHeaderKey,
            DescriptionTextKey = TranslationConstants.UpgradeEnergyRecoveryDescriptionKey,
            BlockingTextKey = requiredReserveLevel is not null
                ? TranslationConstants.UpgradeEnergyRecoveryBlockingTextKey
                : null,
            BlockingTextArgs = [requiredReserveLevel],
            CurrentLevel = (int)energyEntity.RecoveryLevel,
            Price = recoveryParams.NextLevelCost,
            IsStars = false,
            ExecutePathName = EndpointsConstants.UpgradeEnergyRecoveryEndpointName
        };
        return upgradeEntity;
    }
}