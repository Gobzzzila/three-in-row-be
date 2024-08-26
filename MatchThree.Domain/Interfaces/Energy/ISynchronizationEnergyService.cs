using MatchThree.Repository.MSSQL.Models;

namespace MatchThree.Domain.Interfaces.Energy;

public interface ISynchronizationEnergyService
{
    void SynchronizeModel(EnergyDbModel dbModel);
}