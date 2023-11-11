namespace Dexla.Common.BlobStorage.Contracts;

public record BlobStorageModel : IStorageModel
{
    public Stream Data { get; init; } = Stream.Null;
    public string Name { get; init; } = string.Empty;
    public string ContentType { get; init; } = string.Empty;
}