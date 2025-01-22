using System.ComponentModel.DataAnnotations.Schema;
using MatchThree.Repository.MSSQL.Models.Base;
using MatchThree.Shared.Enums;

namespace MatchThree.Repository.MSSQL.Models;

[Table("UserSettings")]
public class UserSettingsDbModel : DbModel
{
    public bool Notifications { get; set; }
    public CultureTypes Culture { get; set; }
    public UserDbModel? User { get; set; }
}