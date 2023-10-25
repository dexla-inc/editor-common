namespace Dexla.Common.Types;

public class HandledException : Exception
{
    public ErrorResponse ErrorResponse { get; }

    public HandledException(ErrorResponse errorResponse)
    {
        ErrorResponse = errorResponse;
    }
}