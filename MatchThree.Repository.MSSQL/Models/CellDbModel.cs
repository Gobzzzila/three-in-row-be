using System.ComponentModel.DataAnnotations.Schema;
using MatchThree.Repository.MSSQL.Interfaces;
using MatchThree.Shared.Enums;

namespace MatchThree.Repository.MSSQL.Models;

[Table("Cells")]
public class CellDbModel : IDbModel
{
    public Guid Id { get; set; }
    public long UserId { get; set; }
    public int CoordinateX { get; set; }
    public int CoordinateY { get; set; }
    public CryptoTypes CellType { get; set; }
    public UserDbModel? User { get; set; }
}
