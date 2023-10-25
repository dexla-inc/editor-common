namespace Dexla.Common.Types;

public class ErrorDetail
{
    public ErrorDetail(string field, string message)
    {
        Field = field;
        Message = message;
    }

    public string Field { get; private set; }
    public string Message { get; private set; }
    public string? ErrorCode { get; init; }
}