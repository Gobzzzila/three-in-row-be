using MatchThree.Shared.Constants;

namespace MatchThree.Shared.Exceptions;

public class NotEnoughBalanceException(string messageKey = NotEnoughBalanceException.DefaultMessageKey)
    : Exception
{
    private const string DefaultMessageKey = TranslationConstants.NotEnoughBalanceExceptionKey;

    public string MessageKey { get; } = messageKey;
}