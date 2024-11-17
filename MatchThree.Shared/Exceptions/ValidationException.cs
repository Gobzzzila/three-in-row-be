using MatchThree.Shared.Constants;

namespace MatchThree.Shared.Exceptions;

public class ValidationException(string messageKey = ValidationException.DefaultMessageKey)
    : Exception
{
    private const string DefaultMessageKey = TranslationConstants.ExceptionValidationTextKey;
    
    public string MessageKey { get; } = messageKey;
}