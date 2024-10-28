using MatchThree.Shared.Enums;

namespace MatchThree.Domain.Configuration;

public class FieldElementParameters
{
    public ElementLevels? NextLevel { get; init; }
    public uint? NextLevelCost { get; init; }
    public int Profit { get; init; }
}