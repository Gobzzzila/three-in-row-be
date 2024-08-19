using System.ComponentModel.DataAnnotations.Schema;
using MatchThree.Repository.MSSQL.Interfaces;

namespace MatchThree.Repository.MSSQL.Models;

[Table("Referrals")]
public class ReferralDbModel : IDbModel
{
    public Guid Id { get; set; }
    public long ReferrerUserId { get; set; }
    public long ReferralUserId { get; set; }
    public bool WasPremium { get; set; }
    public UserDbModel? Referrer { get; set; }
    public UserDbModel? Referral { get; set; }
}