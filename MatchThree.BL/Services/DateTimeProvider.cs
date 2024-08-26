using MatchThree.Domain.Interfaces;

namespace MatchThree.BL.Services;

public sealed class DateTimeProvider : IDateTimeProvider //TODO need to get rid of it, cuz net 8 now has System.TimeProvider
{
    public DateTime GetUtcDateTime()
    {
        return DateTime.UtcNow;
    }
}