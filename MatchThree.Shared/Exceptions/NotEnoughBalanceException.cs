namespace MatchThree.Shared.Exceptions;

public class NotEnoughBalanceException(string messageText = NotEnoughBalanceException.DefaultMessageText)
    : Exception(messageText)
{
    private const string DefaultMessageText = "Not enough balance.";
}