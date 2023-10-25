namespace Dexla.Common.Utilities;

public interface IDateTimeProvider
{
    long GetCurrentUnixTime();
    DateTime GetUtcNow();
}