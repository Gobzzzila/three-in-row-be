using MatchThree.Shared.Enums;

namespace MatchThree.Domain.Models;

public class FieldElementEntity
{
    public Guid Id { get; set; }
    public long UserId { get; set; }
    public CryptoTypes Element { get; set; }
    public ElementLevels Level { get; set; }
}