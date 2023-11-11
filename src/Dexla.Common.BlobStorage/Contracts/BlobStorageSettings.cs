namespace Dexla.Common.BlobStorage.Contracts;

public class BlobStorageSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public string Container { get; set; } = string.Empty;
}