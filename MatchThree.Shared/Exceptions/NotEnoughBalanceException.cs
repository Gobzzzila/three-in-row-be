using MatchThree.Shared.Constants;

namespace MatchThree.Shared.Exceptions;

public class NotEnoughBalanceException(string messageKey = TranslationConstants.ExceptionNotEnoughBalanceTextKey)
    : Exception
{
    public string MessageKey { get; } = messageKey;
}