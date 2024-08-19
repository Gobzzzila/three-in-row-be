using System.ComponentModel.DataAnnotations.Schema;
using MatchThree.Repository.MSSQL.Models.Base;

namespace MatchThree.Repository.MSSQL.Models;

[Table("Balances")]
public class BalanceDbModel : DbModel
{
    public uint Balance { get; set; }
    public ulong OverallBalance { get; set; }
    public UserDbModel? User { get; set; }
}