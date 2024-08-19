using MatchThree.Repository.MSSQL.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatchThree.Repository.MSSQL.Models;

[Table("Users")]
public class UserDbModel : DbModel
{
    public string? Username { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public bool IsPremium { get; set; }
    public DateTime CreatedAt{ get; set; }
    public List<CellDbModel> Cells { get; set; } = [];
    public List<ReferralDbModel> Referrals { get; set; } = [];
    public ReferralDbModel? Referrer { get; set; }
    public BalanceDbModel? Balance { get; set; }
    public EnergyDbModel? Energy { get; set; }
}