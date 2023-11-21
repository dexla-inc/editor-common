using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.BlobStorage;

public class BlobResponse : ISuccess
{
    public string Url { get; set; }
    public BlobResponse(string url)
    {
        Url = url;
    }

    public string TrackingId { get; set; }
}