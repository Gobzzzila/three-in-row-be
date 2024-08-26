namespace MatchThree.Domain.Interfaces.Energy;

public interface IUpdateEnergyService
{
    Task UpgradeReserveAsync(long id);
}