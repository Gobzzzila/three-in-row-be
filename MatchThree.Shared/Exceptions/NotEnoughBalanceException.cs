namespace MatchThree.Shared.Exceptions;

public class NotEnoughBalanceException(string messageKey = NotEnoughBalanceException.DefaultMessageKey)
    : Exception
{
    private const string DefaultMessageKey = "NotEnoughBalanceExceptionKey";

    public string MessageKey { get; } = messageKey;
}