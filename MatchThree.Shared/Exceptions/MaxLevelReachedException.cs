using MatchThree.Shared.Constants;

namespace MatchThree.Shared.Exceptions;

public class MaxLevelReachedException(string messageKey = MaxLevelReachedException.DefaultMessageKey)
    : Exception
{
    private const string DefaultMessageKey = TranslationConstants.MaxLevelReachedTextKey;
    
    public string MessageKey { get; } = messageKey;
}