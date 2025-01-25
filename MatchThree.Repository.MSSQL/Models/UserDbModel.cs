using MatchThree.Repository.MSSQL.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;
using MatchThree.Shared.Constants;

namespace MatchThree.Repository.MSSQL.Models;

[Table(AuthenticationConstants.UserTableName)]
public class UserDbModel : DbModel
{
    public string? Username { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public bool IsPremium { get; set; }
    public string SessionHash { get; set; } = string.Empty;
    public int DailyAdAmount { get; set; }
    public DateTime CreatedAt{ get; set; }
    public DateTime? BannedUntil { get; set; }
    public List<ReferralDbModel> Referrals { get; set; } = [];
    public ReferralDbModel? Referrer { get; set; }
    public BalanceDbModel? Balance { get; set; }
    public EnergyDbModel? Energy { get; set; }
    public FieldDbModel? FieldElementLevel { get; set; }
    public UserSettingsDbModel? Settings { get; set; }
    public NotificationsDbModel? Notifications { get; set; }
}