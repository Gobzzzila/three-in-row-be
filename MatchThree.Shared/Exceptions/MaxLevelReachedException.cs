using MatchThree.Shared.Constants;

namespace MatchThree.Shared.Exceptions;

public class MaxLevelReachedException(string messageKey = TranslationConstants.CommonMaxLevelReachedTextKey)
    : Exception
{
    public string MessageKey { get; } = messageKey;
}