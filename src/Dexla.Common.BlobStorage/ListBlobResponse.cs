using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.BlobStorage;

public class ListBlobResponse : ISuccess
{
    public List<BlobResponse> Files { get; }

    public ListBlobResponse(List<BlobResponse> files)
    {
        Files = files;
    }
    
    public ListBlobResponse(IEnumerable<string> fileNames)
    {
        Files = fileNames.Select(fileName => new BlobResponse(fileName)).ToList();
    }

    public string TrackingId { get; set; }
}