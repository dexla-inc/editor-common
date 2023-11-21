using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.BlobStorage;

public class UploadResponse : ISuccess
{
    public string Url { get; set; }
    public UploadResponse(string url)
    {
        Url = url;
    }

    public string TrackingId { get; set; }
}