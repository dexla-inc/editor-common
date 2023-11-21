using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.BlobStorage;

public class UploadMultipleResponse : ISuccess
{
    public List<UploadResponse> Files { get; }

    public UploadMultipleResponse(List<UploadResponse> files)
    {
        Files = files;
    }

    public string TrackingId { get; set; }
}