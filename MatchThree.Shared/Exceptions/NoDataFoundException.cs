using MatchThree.Shared.Constants;

namespace MatchThree.Shared.Exceptions;

public class NoDataFoundException(string messageKey = NoDataFoundException.DefaultMessageKey)
    : Exception
{ 
    private const string DefaultMessageKey = TranslationConstants.ExceptionNoDataFoundTextKey;
    
    public string MessageKey { get; } = messageKey;
}