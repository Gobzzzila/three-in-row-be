using MatchThree.Shared.Enums;

namespace MatchThree.Domain.Models;

public class TelegramPayloadEntity
{
    public long UserId { get; set; }
    public UpgradeTypes UpgradeType { get; set; }
}