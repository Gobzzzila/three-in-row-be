namespace MatchThree.Shared.Constants;

public static class EnergyConstants
{
    //Energy drinks
    public const int FreeEnergyDrinksPerDay = 1;
    public const int PurchasableEnergyDrinksPerDay = 3;
    public const int EnergyDrinkPrice = 34;
    
    //Energy reserve
    public const int Level1EnergyReserve = 30;
    public const int Level2EnergyReserve = 35;
    public const int Level3EnergyReserve = 40;
    public const int Level4EnergyReserve = 45;
    public const int Level5EnergyReserve = 50;
    public const int Level6EnergyReserve = 55;
    public const int Level7EnergyReserve = 60;
    public const int Level8EnergyReserve = 65;
    public const int Level9EnergyReserve = 70;
    public const int Level10EnergyReserve = 75;
    public const int Level11EnergyReserve = 80;
    public const int Level12EnergyReserve = 85;
    public const int Level13EnergyReserve = 90;
    public const int Level14EnergyReserve = 95;
    public const int Level15EnergyReserve = 100;
    
    public const int Level2EnergyReserveCost = 2_000;
    public const int Level3EnergyReserveCost = 3_000;
    public const int Level4EnergyReserveCost = 5_000;
    public const int Level5EnergyReserveCost = 8_000;
    public const int Level6EnergyReserveCost = 12_000;
    public const int Level7EnergyReserveCost = 17_000;
    public const int Level8EnergyReserveCost = 23_000;
    public const int Level9EnergyReserveCost = 30_000;
    public const int Level10EnergyReserveCost = 35_000;
    public const int Level11EnergyReserveCost = 40_000;
    public const int Level12EnergyReserveCost = 45_000;
    public const int Level13EnergyReserveCost = 50_000;
    public const int Level14EnergyReserveCost = 55_000;
    public const int Level15EnergyReserveCost = 60_000;
    
    public const int Level4EnergyReserveReferralAmount = 1;
    public const int Level7EnergyReserveReferralAmount = 3;
    public const int Level10EnergyReserveReferralAmount = 6;
    public const int Level12EnergyReserveReferralAmount = 7;
    public const int Level14EnergyReserveReferralAmount = 8;

    //Energy recovery
    // public static readonly TimeSpan Level1EnergyRecovery = TimeSpan.FromMinutes(4);
    // public static readonly TimeSpan Level2EnergyRecovery = TimeSpan.FromMinutes(3.5);
    // public static readonly TimeSpan Level3EnergyRecovery = TimeSpan.FromMinutes(3);
    // public static readonly TimeSpan Level4EnergyRecovery = TimeSpan.FromMinutes(2.5);
    // public static readonly TimeSpan Level5EnergyRecovery = TimeSpan.FromMinutes(2);
    // public static readonly TimeSpan Level6EnergyRecovery = TimeSpan.FromMinutes(1.75);
    public static readonly TimeSpan Level1EnergyRecovery = TimeSpan.FromSeconds(30);            //TODO REVERSE AT RELEASE
    public static readonly TimeSpan Level2EnergyRecovery = TimeSpan.FromSeconds(25);
    public static readonly TimeSpan Level3EnergyRecovery = TimeSpan.FromSeconds(20);
    public static readonly TimeSpan Level4EnergyRecovery = TimeSpan.FromSeconds(15);
    public static readonly TimeSpan Level5EnergyRecovery = TimeSpan.FromSeconds(10);
    public static readonly TimeSpan Level6EnergyRecovery = TimeSpan.FromSeconds(5);
    
    public const int Level2EnergyRecoveryCost = 5_000;
    public const int Level3EnergyRecoveryCost = 15_000;
    public const int Level4EnergyRecoveryCost = 25_000;
    public const int Level5EnergyRecoveryCost = 50_000;
    public const int Level6EnergyRecoveryCost = 75_000;
}