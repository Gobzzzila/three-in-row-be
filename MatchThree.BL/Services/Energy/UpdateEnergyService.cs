using AutoMapper;
using MatchThree.Domain.Configuration;
using MatchThree.Domain.Interfaces.Balance;
using MatchThree.Domain.Interfaces.Energy;
using MatchThree.Domain.Models;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using MatchThree.Shared.Exceptions;

namespace MatchThree.BL.Services.Energy;

public class UpdateEnergyService(MatchThreeDbContext context,
    ISynchronizationEnergyService synchronizationEnergyService,
    IUpdateBalanceService updateBalanceService,
    IMapper mapper) 
    : IUpdateEnergyService
{
    public async Task UpgradeReserveAsync(long id)
    {
        var dbModel = await context.Set<EnergyDbModel>().FindAsync(id);
        if (dbModel is null)
            throw new NoDataFoundException();
        
        var reserveParams = EnergyReserveConfiguration.GetParamsByLevel(dbModel!.MaxReserve);
        if (!reserveParams.NextLevel.HasValue)
            throw new MaxLevelReachedException();
        
        await updateBalanceService.SpentBalanceAsync(id, reserveParams.NextLevelCost!.Value);
        
        synchronizationEnergyService.SynchronizeModel(dbModel);
        dbModel.MaxReserve = reserveParams.NextLevel.Value;
        var newMaxReserve = EnergyReserveConfiguration.GetReserveMaxValue(reserveParams.NextLevel.Value);
        dbModel.CurrentReserve += (newMaxReserve - reserveParams.MaxReserve);
        
        context.Set<EnergyDbModel>().Update(dbModel);
    }

    public async Task UpgradeRecoveryAsync(long id)
    {
        var dbModel = await context.Set<EnergyDbModel>().FindAsync(id);
        if (dbModel is null)
            throw new NoDataFoundException();
        
        var recoveryParams = EnergyRecoveryConfiguration.GetParamsByLevel(dbModel!.RecoveryLevel);
        if (!recoveryParams.NextLevel.HasValue)
            throw new MaxLevelReachedException();
        
        await updateBalanceService.SpentBalanceAsync(id, recoveryParams.NextLevelCost!.Value);
        
        synchronizationEnergyService.SynchronizeModel(dbModel);
        dbModel.RecoveryLevel = recoveryParams.NextLevel.Value;
        synchronizationEnergyService.SynchronizeModel(dbModel);

        context.Set<EnergyDbModel>().Update(dbModel);
    }

    public async Task<EnergyEntity> UseEnergyDrinkAsync(long id)
    {
        var dbModel = await context.Set<EnergyDbModel>().FindAsync(id);
        if (dbModel is null)
            throw new NoDataFoundException();
        
        if (dbModel.AvailableEnergyDrinkAmount < 1)
            throw new NotEnoughBalanceException();
        
        var maxReserve = EnergyReserveConfiguration.GetReserveMaxValue(dbModel!.MaxReserve);
        synchronizationEnergyService.SynchronizeModel(dbModel);
        dbModel.CurrentReserve += maxReserve;
        dbModel.LastRecoveryStartTime = null;
        dbModel.AvailableEnergyDrinkAmount -= 1;
        
        var result = context.Set<EnergyDbModel>().Update(dbModel);
        return mapper.Map<EnergyEntity>(result.Entity);
    }
    
    public async Task PurchaseEnergyDrinkAsync(long id)
    {
        var dbModel = await context.Set<EnergyDbModel>().FindAsync(id);
        if (dbModel is null)
            throw new NoDataFoundException();
        
        if (dbModel.PurchasableEnergyDrinkAmount < 1)
            throw new NotEnoughBalanceException();
        
        synchronizationEnergyService.SynchronizeModel(dbModel);
        dbModel.PurchasableEnergyDrinkAmount -=1;
        dbModel.AvailableEnergyDrinkAmount += 1;
        
        context.Set<EnergyDbModel>().Update(dbModel);
    }
}