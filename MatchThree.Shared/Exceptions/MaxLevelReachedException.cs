namespace MatchThree.Shared.Exceptions;

public class MaxLevelReachedException(string messageKey = MaxLevelReachedException.DefaultMessageKey)
    : Exception
{
    private const string DefaultMessageKey = "MaxLevelReachedExceptionKey";
    
    public string MessageKey { get; } = messageKey;
}