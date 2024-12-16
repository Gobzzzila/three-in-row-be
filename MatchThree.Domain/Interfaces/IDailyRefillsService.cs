namespace MatchThree.Domain.Interfaces;

public interface IDailyRefillsService
{
    Task ExecuteRefillEnergyDrinksAsync();
    Task ExecuteRefillAdsAsync();
}