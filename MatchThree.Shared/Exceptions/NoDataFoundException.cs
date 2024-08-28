namespace MatchThree.Shared.Exceptions;

public class NoDataFoundException(string messageText = NoDataFoundException.DefaultMessageText)
    : Exception(messageText)
{
    private const string DefaultMessageText = "Specified item does not exist.";
}