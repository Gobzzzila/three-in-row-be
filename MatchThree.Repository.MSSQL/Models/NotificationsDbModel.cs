using System.ComponentModel.DataAnnotations.Schema;
using MatchThree.Repository.MSSQL.Models.Base;

namespace MatchThree.Repository.MSSQL.Models;

[Table("Notifications")]
public class NotificationsDbModel : DbModel
{
    public DateTime? EnergyNotification { get; set; }
    public UserDbModel? User { get; set; }
}