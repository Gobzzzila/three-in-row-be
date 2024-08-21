using MatchThree.Domain.Interfaces;
using MatchThree.Domain.Interfaces.Energy;
using MatchThree.Domain.Models.Configuration;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;

namespace MatchThree.BL.Services.Energy;

public class SynchronizationEnergyService (MatchThreeDbContext context,
    IDateTimeProvider dateTimeProvider,
    ITransactionService transactionService) : ISynchronizationEnergyService
{
    public async Task SynchronizeEnergyInScopedContextAsync(EnergyDbModel dbModel)
    {
        var now = dateTimeProvider.GetUtcDateTime();
        var timePass = now - dbModel.LastRecoveryStartTime;
        if (timePass < TimeSpan.Zero)
            return;
        
        var recoveryTime = EnergyRecoveryConfiguration.GetRecoveryTime(dbModel.RecoveryLevel);
        var recoveredEnergy = (int)(timePass / recoveryTime)!;
        
        if (recoveredEnergy == 0)
            return;
        
        var reserveMaxValue = EnergyReserveConfiguration.GetReserveMaxValue(dbModel.MaxReserve);
        if (dbModel.CurrentReserve + recoveredEnergy >= reserveMaxValue)
        {
            dbModel.CurrentReserve = reserveMaxValue;
            dbModel.LastRecoveryStartTime = null;
        }
        else
        {
            dbModel.CurrentReserve += recoveredEnergy;        
            dbModel.LastRecoveryStartTime += recoveryTime * recoveredEnergy;
        }

        context.Set<EnergyDbModel>().Update(dbModel);
        await transactionService.Commit();
    }
}