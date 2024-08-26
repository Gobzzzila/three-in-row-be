using MatchThree.Shared.Enums;

namespace MatchThree.Domain.Models;

public class BalanceEntity
{
    public long Id { get; set; }
    public uint Balance { get; set; }
    public ulong OverallBalance { get; set; }
    public int TopSpot { get; set; }
}