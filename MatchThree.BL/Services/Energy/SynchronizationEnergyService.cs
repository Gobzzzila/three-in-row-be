using MatchThree.BL.Configuration;
using MatchThree.Domain.Interfaces.Energy;
using MatchThree.Repository.MSSQL.Models;

namespace MatchThree.BL.Services.Energy;

public class SynchronizationEnergyService (TimeProvider timeProvider) : ISynchronizationEnergyService
{
    private readonly TimeProvider _timeProvider = timeProvider;

    public void SynchronizeModel(EnergyDbModel dbModel)
    {
        if (dbModel.LastRecoveryStartTime is null)
            return;
        
        var now = _timeProvider.GetUtcNow().DateTime;
        var timePass = now - dbModel.LastRecoveryStartTime;
        if (timePass < TimeSpan.Zero) //TODO mb need extra logic
            return;
        
        var recoveryTime = EnergyRecoveryConfiguration.GetRecoveryTime(dbModel.RecoveryLevel);
        var recoveredEnergy = (int)(timePass / recoveryTime);
        
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
    }
}