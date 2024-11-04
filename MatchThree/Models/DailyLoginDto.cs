namespace MatchThree.API.Models;

public class DailyLoginDto
{
    public List<int> Rewards { get; init; } = [];
    public int CurrentIndex { get; init; }
    public int StreakCount { get; init; }
    public bool IsExecutedToday { get; init; }
}