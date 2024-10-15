using MatchThree.BL.Configuration;
using MatchThree.Domain.Interfaces.Balance;
using MatchThree.Domain.Interfaces.Energy;
using MatchThree.Domain.Interfaces.Upgrades;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using MatchThree.Shared.Exceptions;

namespace MatchThree.BL.Services.Energy;

public class UpdateEnergyService(MatchThreeDbContext context,
    ISynchronizationEnergyService synchronizationEnergyService,
    IUpdateBalanceService updateBalanceService,
    IUpgradesRestrictionsService upgradesRestrictionsService) 
    : IUpdateEnergyService
{
    private readonly MatchThreeDbContext _context = context;
    private readonly ISynchronizationEnergyService _synchronizationEnergyService = synchronizationEnergyService;
    private readonly IUpdateBalanceService _updateBalanceService = updateBalanceService;
    private readonly IUpgradesRestrictionsService _upgradesRestrictionsService = upgradesRestrictionsService;

    public async Task UpgradeReserveAsync(long userId)
    {
        var dbModel = await _context.Set<EnergyDbModel>().FindAsync(userId);
        if (dbModel is null)
            throw new NoDataFoundException();
        
        var reserveParams = EnergyReserveConfiguration.GetParamsByLevel(dbModel!.MaxReserve);
        if (!reserveParams.NextLevel.HasValue)
            throw new MaxLevelReachedException();

        if (reserveParams.UpgradeCondition is not null)
        {
            var missingAmountOfReferrals = await reserveParams.UpgradeCondition(_upgradesRestrictionsService, userId);
            if (missingAmountOfReferrals is not null) 
                throw new UpgradeConditionsException();
        }
        
        await _updateBalanceService.SpentBalanceAsync(userId, reserveParams.NextLevelCost!.Value);
        
        _synchronizationEnergyService.SynchronizeModel(dbModel);
        dbModel.MaxReserve = reserveParams.NextLevel.Value;
        var newMaxReserve = EnergyReserveConfiguration.GetReserveMaxValue(reserveParams.NextLevel.Value);
        dbModel.CurrentReserve += (newMaxReserve - reserveParams.MaxReserve);
        
        _context.Set<EnergyDbModel>().Update(dbModel);
    }

    public async Task UpgradeRecoveryAsync(long id)
    {
        var dbModel = await _context.Set<EnergyDbModel>().FindAsync(id);
        if (dbModel is null)
            throw new NoDataFoundException();
        
        var recoveryParams = EnergyRecoveryConfiguration.GetParamsByLevel(dbModel!.RecoveryLevel);
        if (!recoveryParams.NextLevel.HasValue)
            throw new MaxLevelReachedException();

        if (recoveryParams.UpgradeCondition is not null)
        {
            var requiredReserveLevel = recoveryParams.UpgradeCondition!(_upgradesRestrictionsService, dbModel.MaxReserve);
            if (requiredReserveLevel is not null) 
                throw new UpgradeConditionsException();
        }
        
        await _updateBalanceService.SpentBalanceAsync(id, recoveryParams.NextLevelCost!.Value);
        
        _synchronizationEnergyService.SynchronizeModel(dbModel);
        dbModel.RecoveryLevel = recoveryParams.NextLevel.Value;
        _synchronizationEnergyService.SynchronizeModel(dbModel);

        _context.Set<EnergyDbModel>().Update(dbModel);
    }

    public async Task UseEnergyDrinkAsync(long id)
    {
        var dbModel = await _context.Set<EnergyDbModel>().FindAsync(id);
        if (dbModel is null)
            throw new NoDataFoundException();
        
        if (dbModel.AvailableEnergyDrinkAmount < 1)
            throw new NotEnoughBalanceException();
        
        var maxReserve = EnergyReserveConfiguration.GetReserveMaxValue(dbModel!.MaxReserve);
        _synchronizationEnergyService.SynchronizeModel(dbModel);
        dbModel.CurrentReserve += maxReserve;
        dbModel.LastRecoveryStartTime = null;
        dbModel.AvailableEnergyDrinkAmount -= 1;
        
        _context.Set<EnergyDbModel>().Update(dbModel);
    }
    
    public async Task PurchaseEnergyDrinkAsync(long id)
    {
        var dbModel = await _context.Set<EnergyDbModel>().FindAsync(id);
        if (dbModel is null)
            throw new NoDataFoundException();
        
        if (dbModel.PurchasableEnergyDrinkAmount < 1)
            throw new NotEnoughBalanceException();
        
        _synchronizationEnergyService.SynchronizeModel(dbModel);
        dbModel.PurchasableEnergyDrinkAmount -=1;
        dbModel.AvailableEnergyDrinkAmount += 1;
        
        _context.Set<EnergyDbModel>().Update(dbModel);
    }
}