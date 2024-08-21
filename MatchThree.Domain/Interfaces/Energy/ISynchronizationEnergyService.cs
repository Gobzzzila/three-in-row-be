using MatchThree.Repository.MSSQL.Models;

namespace MatchThree.Domain.Interfaces.Energy;

public interface ISynchronizationEnergyService
{
    Task SynchronizeEnergyInScopedContextAsync(EnergyDbModel dbModel);
}