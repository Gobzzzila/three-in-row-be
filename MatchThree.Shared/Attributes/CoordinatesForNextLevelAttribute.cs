namespace MatchThree.Shared.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public class CoordinatesForNextLevelAttribute(int x = 0, int y = 0) : Attribute
{
    public int X { get; } = x;
    public int Y { get; } = y;
}