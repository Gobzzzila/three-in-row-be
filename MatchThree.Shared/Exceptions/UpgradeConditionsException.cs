using MatchThree.Shared.Constants;

namespace MatchThree.Shared.Exceptions;

public class UpgradeConditionsException(string messageKey = UpgradeConditionsException.DefaultMessageKey)
    : Exception
{
    private const string DefaultMessageKey = TranslationConstants.ExceptionUpgradeConditionsTextKey;
    
    public string MessageKey { get; } = messageKey;
}