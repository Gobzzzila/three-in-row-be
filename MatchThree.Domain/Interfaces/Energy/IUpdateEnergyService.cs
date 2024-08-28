using MatchThree.Domain.Models;

namespace MatchThree.Domain.Interfaces.Energy;

public interface IUpdateEnergyService
{
    Task UpgradeReserveAsync(long userId);
    Task UpgradeRecoveryAsync(long id);
    Task<EnergyEntity> UseEnergyDrinkAsync(long id);
    Task PurchaseEnergyDrinkAsync(long id);
}