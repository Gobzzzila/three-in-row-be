namespace MatchThree.API.Models.Upgrades;

public class GroupedUpgradesDto
{
    public string CategoryName { get; init; } = string.Empty;
    public string Category { get; init; } = string.Empty;
    
    public List<UpgradeDto> Upgrades { get; init; } = [];
}