using MatchThree.Shared.Enums;

namespace MatchThree.Domain.Models.Upgrades;

public class GroupedUpgradesEntity
{
    public UpgradeCategories Category { get; set; }
    
    public List<UpgradeEntity> Upgrades { get; set; } = [];
}