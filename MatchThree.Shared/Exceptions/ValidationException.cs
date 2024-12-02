using System.Net;
using MatchThree.Shared.Constants;

namespace MatchThree.Shared.Exceptions;

public class ValidationException(string messageKey = TranslationConstants.ExceptionValidationTextKey)
    : Exception
{
    public ValidationException(string messageKey, object?[] messageArgs) 
        : this(messageKey)
    {
        MessageArgs = messageArgs;
    }

    public string MessageKey { get; } = messageKey;
    public object?[] MessageArgs { get; } = [];
}