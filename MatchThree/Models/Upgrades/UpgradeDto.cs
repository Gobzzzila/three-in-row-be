namespace MatchThree.API.Models.Upgrades;

public class UpgradeDto
{
    public string HeaderText { get; set; } = string.Empty;
    public string DescriptionText { get; set; } = string.Empty; //TODO mb redo to init;
    public string? BlockingText { get; set; }
    public int CurrentLevel { get; init; }
    public uint? Price { get; init; }
    public bool IsStars { get; init; }
    public string Type { get; init; } = string.Empty;
    public string ExecutePath { get; init; } = string.Empty;
}