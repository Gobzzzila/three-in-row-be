namespace MatchThree.Domain.Interfaces.Energy;

public interface IEnergyDrinkRefillsService
{
    Task RefillFreeEnergyDrinks();
    
    Task RefillPurchasableEnergyDrinks();
}