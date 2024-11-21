using MatchThree.Shared.Constants;

namespace MatchThree.Shared.Exceptions;

public class UpgradeConditionsException(string messageKey = TranslationConstants.ExceptionUpgradeConditionsTextKey)
    : Exception
{
    public string MessageKey { get; } = messageKey;
}