using MatchThree.Domain.Interfaces.Balance;
using MatchThree.Domain.Interfaces.Energy;
using MatchThree.Domain.Models.Configuration;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using MatchThree.Shared.Exceptions;

namespace MatchThree.BL.Services.Energy;

public class UpdateEnergyService(MatchThreeDbContext context,
    ISynchronizationEnergyService synchronizationEnergyService,
    IUpdateBalanceService updateBalanceService) 
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
}