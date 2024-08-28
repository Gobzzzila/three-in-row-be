namespace MatchThree.Shared.Exceptions;

public class MaxLevelReachedException(string messageText = MaxLevelReachedException.DefaultMessageText)
    : Exception(messageText)
{
    private const string DefaultMessageText = "Max level reached.";
}