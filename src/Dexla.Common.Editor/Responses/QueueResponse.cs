using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Responses;

public class QueueResponse : ISuccess
{
    public string Message { get; set; } = "Message processing";
    public static readonly QueueResponse Instance = new();
    public string TrackingId { get; set; } = string.Empty;
}