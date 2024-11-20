using MatchThree.Shared.Enums;

namespace MatchThree.Shared.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public class NextLevelFieldInfoAttribute(int x = 0, int y = 0, CryptoTypes newCrypto = CryptoTypes.Undefined) 
    : Attribute
{
    public int X { get; } = x;
    public int Y { get; } = y;
    public CryptoTypes? NewCrypto { get; } = newCrypto == CryptoTypes.Undefined ? null : newCrypto;
}