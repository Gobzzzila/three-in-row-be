namespace MatchThree.Domain.Configuration;

public class FieldElementParameters
{
    public FieldElementParameters? NextLevelParams { get; init; }
    public uint? NextLevelCost { get; init; }
    public int Profit { get; init; }
}