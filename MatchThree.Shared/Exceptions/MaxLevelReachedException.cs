using MatchThree.Shared.Constants;

namespace MatchThree.Shared.Exceptions;

public class MaxLevelReachedException(string messageKey = MaxLevelReachedException.DefaultMessageKey)
    : Exception
{
    private const string DefaultMessageKey = TranslationConstants.MaxLevelReachedExceptionKey;
    
    public string MessageKey { get; } = messageKey;
}