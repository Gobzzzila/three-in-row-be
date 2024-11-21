using MatchThree.Shared.Constants;

namespace MatchThree.Shared.Exceptions;

public class NoDataFoundException(string messageKey = TranslationConstants.ExceptionNoDataFoundTextKey)
    : Exception
{ 
    public string MessageKey { get; } = messageKey;
}