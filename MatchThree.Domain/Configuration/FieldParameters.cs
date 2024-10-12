using MatchThree.Shared.Enums;

namespace MatchThree.Domain.Configuration;

public class FieldParameters
{
    public int AmountOfCells { get; init; }
    public FieldLevels? NextLevel { get; init; }
    public uint? NextLevelCost { get; init; }
}