namespace MatchThree.Domain.Interfaces.Energy;

public interface IDeleteEnergyService
{
    Task DeleteAsync(long id);
}