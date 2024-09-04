namespace MatchThree.Shared.Exceptions;

public class UpgradeConditionsException(string messageKey = UpgradeConditionsException.DefaultMessageKey)
    : Exception
{
    private const string DefaultMessageKey = "UpgradeConditionsExceptionKey";
    
    public string MessageKey { get; } = messageKey;
}