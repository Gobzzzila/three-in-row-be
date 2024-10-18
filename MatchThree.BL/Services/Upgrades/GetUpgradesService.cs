using MatchThree.BL.Configuration;
using MatchThree.Domain.Interfaces.Energy;
using MatchThree.Domain.Interfaces.FieldElements;
using MatchThree.Domain.Interfaces.Upgrades;
using MatchThree.Domain.Models;
using MatchThree.Domain.Models.Upgrades;
using MatchThree.Shared.Constants;
using MatchThree.Shared.Enums;

namespace MatchThree.BL.Services.Upgrades;

public class GetUpgradesService(IUpgradesRestrictionsService upgradesRestrictionsService, 
    IReadEnergyService readEnergyService,
    IReadFieldElementsService readFieldElementsService,
    TimeProvider timeProvider) 
    : IGetUpgradesService
{
    private readonly IUpgradesRestrictionsService _upgradesRestrictionsService = upgradesRestrictionsService;
    private readonly IReadEnergyService _readEnergyService = readEnergyService;
    private readonly IReadFieldElementsService _readFieldElementsService = readFieldElementsService;
    private readonly TimeProvider _timeProvider = timeProvider;

    public async Task<IReadOnlyCollection<GroupedUpgradesEntity>> GetAll(long userId)
    {
        var result = new List<GroupedUpgradesEntity>
        {
            await GetEnergyUpgrades(userId),
            await GetFieldElementsUpgrades(userId)
        };
        
        return result;
    }
    
    #region EnergyUpgrades
    
    private async Task<GroupedUpgradesEntity> GetEnergyUpgrades(long userId)
    {
        var energyEntity = await _readEnergyService.GetByUserIdAsync(userId);

        var result = new GroupedUpgradesEntity
        {
            Category = UpgradeCategories.Energy,
            Upgrades =
            [
                GetEnergyDrinkUpgrade(energyEntity),
                await GetEnergyReserveUpgrade(energyEntity),
                GetEnergyRecoveryUpgrade(energyEntity)
            ]
        };

        return result;
    }

    private UpgradeEntity GetEnergyDrinkUpgrade(EnergyEntity energyEntity)
    {
        var condition = energyEntity.AvailableEnergyDrinkAmount > 0;
        var upgradeEntity = new UpgradeEntity
        {
            Type = UpgradeTypes.EnergyDrink,
            BlockingTextArgs = energyEntity.AvailableEnergyDrinkAmount + energyEntity.PurchasableEnergyDrinkAmount < 1
                ? [Math.Ceiling((_timeProvider.GetUtcNow().Date.AddDays(1) - _timeProvider.GetUtcNow()).TotalHours)]
                : [],
            CurrentLevel = energyEntity.AvailableEnergyDrinkAmount + energyEntity.PurchasableEnergyDrinkAmount,
            Price = condition 
                ? 0 
                : (uint)EnergyConstants.EnergyDrinkPrice,
            IsStars = !condition,
            ExecutePathName = condition
                ? EndpointsConstants.UseEnergyDrinkEndpoint
                : EndpointsConstants.CreateInvoiceLinkEndpoint,
            ExecutePathArgs = condition 
                ? new { userId = energyEntity.Id } 
                : new { userId = energyEntity.Id, upgradeType = (int)UpgradeTypes.EnergyDrink }     //TODO get rid of anonymous cringe
        };
        return upgradeEntity;
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
            Type = UpgradeTypes.EnergyReserve,
            BlockingTextArgs = missingAmountOfReferrals is not null
                ? [missingAmountOfReferrals]
                : [],
            CurrentLevel = (int)energyEntity.MaxReserve,
            Price = reserveParams.NextLevelCost,
            IsStars = false,
            ExecutePathName = EndpointsConstants.UpgradeEnergyReserveEndpointName,
            ExecutePathArgs = new { userId = energyEntity.Id }                                      //TODO get rid of anonymous cringe
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
            Type = UpgradeTypes.EnergyRecovery,
            BlockingTextArgs = requiredReserveLevel is not null
                ? [requiredReserveLevel]
                : [],
            CurrentLevel = (int)energyEntity.RecoveryLevel,
            Price = recoveryParams.NextLevelCost,
            IsStars = false,
            ExecutePathName = EndpointsConstants.UpgradeEnergyRecoveryEndpointName,
            ExecutePathArgs = new { userId = energyEntity.Id }                              //TODO get rid of anonymous cringe
        };
        return upgradeEntity;
    }
    
    #endregion EnergyUpgrades
    
    #region FieldElementsUpgrades
    
    private async Task<GroupedUpgradesEntity> GetFieldElementsUpgrades(long userId)
    {
        var fieldElements = await _readFieldElementsService.GetByUserIdAsync(userId);
        
        var result = new GroupedUpgradesEntity
        {
            Category = UpgradeCategories.Field,
            Upgrades =
            [
                GetFieldUpgrade(fieldElements),
            ]
        };

        return result;
    }

    private static UpgradeEntity GetFieldUpgrade(FieldElementsEntity fieldElements)
    {
        var fieldParams = FieldConfiguration.GetParamsByLevel(fieldElements.FieldLevel);

        var upgradeEntity = new UpgradeEntity
        {
            Type = UpgradeTypes.Field,
            CurrentLevel = (int)fieldElements.FieldLevel,
            Price = fieldParams.NextLevelCost,
            IsStars = false,
            ExecutePathName = EndpointsConstants.UpgradeFieldEndpointName,
            ExecutePathArgs = new { userId = fieldElements.Id }                                 //TODO get rid of anonymous cringe
        };
        return upgradeEntity;
    }

    #endregion FieldElementsUpgrades
}