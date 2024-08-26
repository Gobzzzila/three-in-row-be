namespace MatchThree.Shared.Exceptions;

public class MaxLevelReachedException() : Exception(DefaultMessageText)
{
    private const string DefaultMessageText = "Max level reached.";
}