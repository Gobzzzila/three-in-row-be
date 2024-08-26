namespace MatchThree.Shared.Exceptions;

public class NoDataFoundException() : Exception(DefaultMessageText)
{
    private const string DefaultMessageText = "Specified item does not exist.";
}