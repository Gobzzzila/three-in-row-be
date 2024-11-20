using MatchThree.BL.Configuration;
using MatchThree.Domain.Interfaces.Energy;
using MatchThree.Domain.Interfaces.Field;
using MatchThree.Domain.Interfaces.FieldElement;
using MatchThree.Domain.Interfaces.Upgrades;
using MatchThree.Domain.Models;
using MatchThree.Domain.Models.Upgrades;
using MatchThree.Shared.Constants;
using MatchThree.Shared.Enums;

namespace MatchThree.BL.Services.Upgrades;

public class GetUpgradesService(IUpgradesRestrictionsService upgradesRestrictionsService, 
    IReadEnergyService readEnergyService,
    IReadFieldService readFieldService,
    IReadFieldElementService readFieldElementService) 
    : IGetUpgradesService
{
    private readonly IUpgradesRestrictionsService _upgradesRestrictionsService = upgradesRestrictionsService;
    private readonly IReadEnergyService _readEnergyService = readEnergyService;
    private readonly IReadFieldService _readFieldService = readFieldService;
    private readonly IReadFieldElementService _readFieldElementService = readFieldElementService;

    public async Task<IReadOnlyCollection<GroupedUpgradesEntity>> GetAll(long userId)
    {
        var energyEntity = await _readEnergyService.GetByUserIdAsync(userId);
        var field = await _readFieldService.GetByUserIdAsync(userId);
        var fieldElements = await _readFieldElementService.GetByUserIdAsync(userId);

        var result = new List<GroupedUpgradesEntity>
        {
            await GetEnergyUpgrades(energyEntity),
            GetFieldUpgrades(field),
            await GetFieldElementsUpgrades(fieldElements)
        };
        
        return result;
    }
    
    #region EnergyUpgrades
    
    private async ValueTask<GroupedUpgradesEntity> GetEnergyUpgrades(EnergyEntity energyEntity)
    {
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

    private static UpgradeEntity GetEnergyDrinkUpgrade(EnergyEntity energyEntity)
    {
        var condition = energyEntity.AvailableEnergyDrinkAmount > 0;
        var reserveParams = EnergyReserveConfiguration.GetParamsByLevel(energyEntity.MaxReserve);
        var availableAmount = energyEntity.AvailableEnergyDrinkAmount + energyEntity.PurchasableEnergyDrinkAmount;
        
        var upgradeEntity = new UpgradeEntity
        {
            Type = UpgradeTypes.EnergyDrink,
            DescriptionTextArgs = [$"{reserveParams.MaxReserve}"],
            IsBlocked = availableAmount < 1,
            CurrentLevel = availableAmount,
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
            missingAmountOfReferrals = await reserveParams.UpgradeCondition(_upgradesRestrictionsService, energyEntity.Id);

        var upgradeEntity = new UpgradeEntity
        {
            Type = UpgradeTypes.EnergyReserve,
            IsBlocked = missingAmountOfReferrals is not null,
            BlockingTextArgs = [missingAmountOfReferrals],
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
            requiredReserveLevel = recoveryParams.UpgradeCondition(_upgradesRestrictionsService, energyEntity.MaxReserve);
        
        var upgradeEntity = new UpgradeEntity
        {
            Type = UpgradeTypes.EnergyRecovery,
            IsBlocked = requiredReserveLevel is not null,
            BlockingTextArgs = [requiredReserveLevel],
            CurrentLevel = (int)energyEntity.RecoveryLevel,
            Price = recoveryParams.NextLevelCost,
            IsStars = false,
            ExecutePathName = EndpointsConstants.UpgradeEnergyRecoveryEndpointName,
            ExecutePathArgs = new { userId = energyEntity.Id }                              //TODO get rid of anonymous cringe
        };
        return upgradeEntity;
    }
    
    #endregion EnergyUpgrades
    
    #region FieldUpgrades
    
    private static GroupedUpgradesEntity GetFieldUpgrades(FieldEntity field)
    {
        var result = new GroupedUpgradesEntity
        {
            Category = UpgradeCategories.Field,
            Upgrades =
            [
                GetFieldUpgrade(field),
            ]
        };

        return result;
    }

    private static UpgradeEntity GetFieldUpgrade(FieldEntity field)
    {
        var fieldParams = FieldConfiguration.GetParamsByLevel(field.FieldLevel);

        var upgradeEntity = new UpgradeEntity
        {
            Type = UpgradeTypes.Field,
            CurrentLevel = (int)field.FieldLevel,
            Price = fieldParams.NextLevelCost,
            IsStars = false,
            ExecutePathName = EndpointsConstants.UpgradeFieldEndpointName,
            ExecutePathArgs = new { userId = field.Id }                                 //TODO get rid of anonymous cringe
        };
        return upgradeEntity;
    }

    #endregion FieldElementsUpgrades
    
    #region FieldElementsUpgrades
    
    private async ValueTask<GroupedUpgradesEntity> GetFieldElementsUpgrades(List<FieldElementEntity> fieldElements)
    {
        var result = new GroupedUpgradesEntity
        {
            Category = UpgradeCategories.FieldElements,
            Upgrades = new List<UpgradeEntity>(fieldElements.Count)
        };

        foreach (var filedElement in fieldElements)
        {
            var upgradeEntity = await GetFieldElementUpgrade(filedElement);
            result.Upgrades.Add(upgradeEntity);
        }

        return result;
    }

    private async ValueTask<UpgradeEntity> GetFieldElementUpgrade(FieldElementEntity fieldElement)
    {
        var fieldElementParams = FieldElementsConfiguration.GetParamsByTypeAndLevel(fieldElement.Element, fieldElement.Level);

        int? restrictedFieldLevel = default;
        if (fieldElementParams.UpgradeCondition is not null)
            restrictedFieldLevel = await fieldElementParams.UpgradeCondition(_upgradesRestrictionsService, fieldElement.UserId);

        if (fieldElement.Level == ElementLevels.Undefined)
            restrictedFieldLevel = (int)FieldElementsConfiguration.GetRequiredFieldLevelForFirstLevelElement(fieldElement.Element);
        
        var upgradeEntity = new UpgradeEntity
        {
            HeaderTextArgs = [fieldElement.Element.ToString()],
            DescriptionTextArgs =
            [
                fieldElement.Element.ToString(), 
                fieldElementParams.Profit,
                FieldElementsConfiguration.GetNextLevelProfit(fieldElement.Element, fieldElement.Level)
            ],
            BlockingTextArgs = [restrictedFieldLevel],
            IsBlocked = restrictedFieldLevel is not null,
            Type = ConvertCryptoTypeToUpgradeType(fieldElement.Element),
            CurrentLevel = (int)fieldElement.Level,
            Price = fieldElementParams.NextLevelCost,
            IsStars = false,
            ExecutePathName = EndpointsConstants.UpgradeFieldElementEndpointName,
            ExecutePathArgs = new { userId = fieldElement.UserId, fieldElement = (int)fieldElement.Element }      //TODO get rid of anonymous cringe
        };
        return upgradeEntity;

        UpgradeTypes ConvertCryptoTypeToUpgradeType(CryptoTypes cryptoType)
        {
            return cryptoType switch
            {
                CryptoTypes.Ton => UpgradeTypes.TonElement,
                CryptoTypes.Ston => UpgradeTypes.StonElement,
                CryptoTypes.Raff => UpgradeTypes.RaffElement,
                CryptoTypes.Fnz => UpgradeTypes.FnzElement,
                CryptoTypes.Usdt => UpgradeTypes.UsdtElement,
                CryptoTypes.Jetton => UpgradeTypes.JettonElement,
                CryptoTypes.Not => UpgradeTypes.NotElement,
                CryptoTypes.Dogs => UpgradeTypes.DogsElement,
                CryptoTypes.Cati => UpgradeTypes.CatiElement,
                _ => UpgradeTypes.Undefined
            };
        }
    }
    
    #endregion FieldElementsUpgrades
}