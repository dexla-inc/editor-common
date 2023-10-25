using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Types;

public record DownloadResponse(
    string FileName, 
    byte[] FileContent,
    string MediaType) : ISuccess
{
    public string TrackingId { get; set; }
}