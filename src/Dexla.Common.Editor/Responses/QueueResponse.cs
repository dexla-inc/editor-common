using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Responses;

public class QueueResponse : ISuccess
{
    public string Message { get; set; } = "Your request has been accepted for processing.";
    public string CallbackUrl { get; }
    public string StatusEndpoint { get; }
    public long EstimatedTimeOfCompletion { get; }
    public Dictionary<string, string> ExpectedCallback { get; }

    public QueueResponse(
        string callbackUrl,
        string statusEndpoint,
        long estimatedTimeOfCompletion,
        Dictionary<string, string> expectedCallback)
    {
        CallbackUrl = callbackUrl;
        StatusEndpoint = statusEndpoint;
        EstimatedTimeOfCompletion = estimatedTimeOfCompletion;
        ExpectedCallback = expectedCallback;
    }

    public string TrackingId { get; set; } = string.Empty;
}