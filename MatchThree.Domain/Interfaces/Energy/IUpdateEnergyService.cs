namespace MatchThree.Domain.Interfaces.Energy;

public interface IUpdateEnergyService
{
    Task UpgradeReserveAsync(long userId);
    Task UpgradeRecoveryAsync(long id);
    Task UseEnergyDrinkAsync(long id);
    Task PurchaseEnergyDrinkAsync(long id);
}