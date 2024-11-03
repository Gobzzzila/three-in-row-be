using System.ComponentModel.DataAnnotations.Schema;
using MatchThree.Repository.MSSQL.Interfaces;

namespace MatchThree.Repository.MSSQL.Models;

[Table("CompletedQuests")]
public class CompletedQuestsDbModel : IDbModel
{
    public long Id { get; set; }
    public List<Guid> QuestIds { get; set; } = [];
}