namespace MatchThree.Domain.Interfaces.FieldElements;

public interface IUpdateFieldElementsService
{
    Task UpgradeFieldAsync(long userId);
}