using MatchThree.Shared.Enums;

namespace MatchThree.Domain.Models;

public class FieldEntity
{
    public long Id { get; set; }
    public FieldLevels FieldLevel { get; set; }
}