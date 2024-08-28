using MatchThree.Domain.Configuration;
using MatchThree.Domain.Interfaces;
using MatchThree.Domain.Interfaces.Energy;
using MatchThree.Repository.MSSQL.Models;

namespace MatchThree.BL.Services.Energy;

public class SynchronizationEnergyService (IDateTimeProvider dateTimeProvider) : ISynchronizationEnergyService
{
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;

    public void SynchronizeModel(EnergyDbModel dbModel)
    {
        if (dbModel.LastRecoveryStartTime is null)
            return;
        
        var now = _dateTimeProvider.GetUtcDateTime();
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