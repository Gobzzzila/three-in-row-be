namespace MatchThree.Domain.Interfaces.Energy;

public interface ICreateEnergyService
{
    Task CreateAsync(long userId);
}