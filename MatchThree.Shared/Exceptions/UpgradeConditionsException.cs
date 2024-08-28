namespace MatchThree.Shared.Exceptions;

public class UpgradeConditionsException(string messageText = UpgradeConditionsException.DefaultMessageText)
    : Exception(messageText)
{
    private const string DefaultMessageText = "Improvement conditions have not been met.";
}