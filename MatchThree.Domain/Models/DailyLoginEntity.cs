namespace MatchThree.Domain.Models;

public class DailyLoginEntity
{
    public List<int> Rewards { get; set; } = [];
    public int CurrentIndex { get; set; }
    public int StreakCount { get; set; }
    public bool IsExecutedToday { get; set; }
}