namespace MatchThree.Shared.Exceptions;

public class NoDataFoundException(string messageKey = NoDataFoundException.DefaultMessageKey)
    : Exception
{ 
    private const string DefaultMessageKey = "NoDataFoundExceptionKey";
    
    public string MessageKey { get; } = messageKey;
}