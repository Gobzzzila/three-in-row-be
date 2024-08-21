using MatchThree.Domain.Models;

namespace MatchThree.Domain.Interfaces.Energy;

public interface IReadEnergyService
{
    Task<EnergyEntity> GetByUserIdAsync(long userId);
}