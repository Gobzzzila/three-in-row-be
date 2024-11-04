using System.ComponentModel.DataAnnotations.Schema;
using MatchThree.Repository.MSSQL.Models.Base;

namespace MatchThree.Repository.MSSQL.Models;

[Table("DailyLogins")]
public class DailyLoginDbModel : DbModel
{
    public DateTime LastExecuteDate { get; set; } = DateTime.MinValue;
    public int StreakCount { get; set; }
}