using MatchThree.Shared.Enums;

namespace MatchThree.Domain.Models;

public class UserSettingsEntity
{
    public long Id { get; set; }
    public bool Notifications { get; set; }
    public CultureTypes Culture { get; set; }
}