using System.ComponentModel.DataAnnotations.Schema;
using MatchThree.Repository.MSSQL.Interfaces;
using MatchThree.Shared.Enums;

namespace MatchThree.Repository.MSSQL.Models;

[Table("FieldElements")]
public class FieldElementDbModel : IDbModel
{
    public Guid Id { get; set; }
    public long UserId { get; set; }
    public CryptoTypes Element { get; set; }
    public ElementLevels Level { get; set; }
    
    public FieldDbModel? Field { get; set; }
}