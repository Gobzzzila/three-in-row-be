namespace MatchThree.API.Models;

public sealed class MoveInfoRequestDto
{
    public int Reward { get; init; }
    public int[][] Field { get; init; } = [];
    public string Hash { get; init; } = string.Empty;
}