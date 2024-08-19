using MatchThree.Domain.Interfaces;

namespace MatchThree.BL.Services;

public sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime GetUtcDateTime()
    {
        return DateTime.UtcNow;
    }
}