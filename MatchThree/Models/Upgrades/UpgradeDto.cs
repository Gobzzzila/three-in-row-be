namespace MatchThree.API.Models.Upgrades;

public class UpgradeDto
{
    public string HeaderText { get; init; } = string.Empty;
    public string DescriptionText { get; init; } = string.Empty;
    public string? BlockingText { get; init; }
    public int CurrentLevel { get; init; }
    public uint? Price { get; init; }
    public bool IsStars { get; init; }
    public string ExecutePath { get; set; } = string.Empty;
}