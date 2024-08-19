using MatchThree.Repository.MSSQL.Interfaces;

namespace MatchThree.Repository.MSSQL.Models.Base;

public class DbModel : IDbModel
{
    public long Id { get; set; }
}