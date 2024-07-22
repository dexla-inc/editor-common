namespace Dexla.Common.BlobStorage.Contracts;

public record BlobStorageModel : IStorageModel
{
    public Stream Data { get; init; } = Stream.Null;
    public string Name { get; init; } = string.Empty;
    public string ContentType { get; init; } = string.Empty;
    /// <summary>
    /// Gets the quality, that will be used to encode the image. Quality index must be between 1 and 100 (compression from max to min).
    /// </summary>
    public int CompressionLevel { get; init; }
}