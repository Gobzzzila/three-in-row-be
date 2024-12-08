using System.ComponentModel.DataAnnotations.Schema;
using MatchThree.Repository.MSSQL.Models.Base;
using MatchThree.Shared.Constants;
using MatchThree.Shared.Enums;

namespace MatchThree.Repository.MSSQL.Models;

[Table(LeaderBoardConstants.LeaderBoardTableName)]
public class LeaderboardMemberDbModel : DbModel
{
    public LeagueTypes League { get; set; }
    public int TopSpot { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public ulong OverallBalance { get; set; }
}