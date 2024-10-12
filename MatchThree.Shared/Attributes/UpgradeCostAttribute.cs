namespace MatchThree.Shared.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public sealed class UpgradeCostAttribute : Attribute
{
    public UpgradeCostAttribute()
    {
        UpgradeCost = null;
    }
    
    public UpgradeCostAttribute(uint upgradeCost)
    {
        UpgradeCost = upgradeCost;
    }

    public uint? UpgradeCost { get; }
}