using MatchThree.Shared.Enums;

namespace MatchThree.Domain.Interfaces.FieldElement;

public interface IUpdateFieldElementService
{
    Task UpgradeFieldElementAsync(long userId, CryptoTypes cryptoType);
    
    Task UnlockFieldElementAsync(long userId, CryptoTypes cryptoType);
}