namespace Dexla.Common.Utilities;

public class DateTimeProvider : IDateTimeProvider
{
    public long GetCurrentUnixTime()
    {
        return DateTime.UtcNow.ToUnixTimeMilliseconds();
    }

    public DateTime GetUtcNow()
    {
        return DateTime.UtcNow;
    }
}