namespace MatchThree.Shared.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public sealed class UpgradeCostAttribute(uint upgradeCost) : Attribute
{
    public uint UpgradeCost { get; } = upgradeCost;
}