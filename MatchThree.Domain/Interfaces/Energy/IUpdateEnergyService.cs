namespace MatchThree.Domain.Interfaces.Energy;

public interface IUpdateEnergyService
{
    Task UpgradeReserveAsync(long id);
    Task UpgradeRecoveryAsync(long id);
}