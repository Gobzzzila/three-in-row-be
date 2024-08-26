namespace MatchThree.Shared.Exceptions;

public class NotEnoughBalanceException() : Exception(DefaultMessageText)
{
    private const string DefaultMessageText = "Not enough balance.";
}