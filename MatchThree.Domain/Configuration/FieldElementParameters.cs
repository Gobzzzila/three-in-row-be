using MatchThree.Shared.Enums;

namespace MatchThree.Domain.Configuration;

public class FieldElementParameters
{
    public uint? NextLevelCost { get; init; }
    public ElementLevels? NextLevel { get; init; }
    public int Profit { get; init; }
}