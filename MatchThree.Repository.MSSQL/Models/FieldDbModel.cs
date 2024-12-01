using System.ComponentModel.DataAnnotations.Schema;
using MatchThree.Repository.MSSQL.Models.Base;
using MatchThree.Shared.Enums;

namespace MatchThree.Repository.MSSQL.Models;

[Table("Fields")]
public class FieldDbModel : DbModel
{
    public FieldLevels FieldLevel { get; set; }
    public int[][] Field { get; set; } = [];
    public int MoveСounter { get; set; }
    public UserDbModel? User { get; set; }
    public List<FieldElementDbModel>? FieldElements { get; set; } = [];
}