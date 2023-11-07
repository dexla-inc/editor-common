using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Responses;

public class QueueResponse : ISuccess
{
    public string Message { get; } = "Your request has been accepted for processing.";
    public string CallbackUrl { get; set; } = string.Empty;
    public string StatusEndpoint { get; set; } = string.Empty;
    public long EstimatedTimeOfCompletion { get; set; }

    public Dictionary<string, string> ExpectedCallback { get; set; } = new()
    {
        {
            "onSuccess",
            "The callback URL will receive a POST request with the result upon successful processing."
        },
        {
            "onFailure", "The callback URL will receive a POST request with error details if processing fails."
        },
        {
            "onProgress",
            "The callback URL may receive periodic POST requests if there are multiple steps involved in the processing."
        }
    };
    
    public string TrackingId { get; set; } = string.Empty;

    public QueueResponse()
    {
    }
}