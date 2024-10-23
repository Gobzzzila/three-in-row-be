namespace MatchThree.Domain.Interfaces.Energy;

public interface IUpdateEnergyService
{
    Task UpgradeReserveAsync(long userId);
    Task UpgradeRecoveryAsync(long userId);
    Task UseEnergyDrinkAsync(long userId);
    Task PurchaseEnergyDrinkAsync(long userId);
    Task SpendEnergy(long userId);
}