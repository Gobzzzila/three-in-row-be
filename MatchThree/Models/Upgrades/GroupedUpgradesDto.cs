namespace MatchThree.API.Models.Upgrades;

public class GroupedUpgradesDto
{
    public string CategoryName { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    
    public List<UpgradeDto> Upgrades { get; set; } = [];
}