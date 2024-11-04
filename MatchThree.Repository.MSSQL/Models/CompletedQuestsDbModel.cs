using System.ComponentModel.DataAnnotations.Schema;
using MatchThree.Repository.MSSQL.Models.Base;

namespace MatchThree.Repository.MSSQL.Models;

[Table("CompletedQuests")]
public class CompletedQuestsDbModel : DbModel
{
    public List<Guid> QuestIds { get; set; } = [];
}