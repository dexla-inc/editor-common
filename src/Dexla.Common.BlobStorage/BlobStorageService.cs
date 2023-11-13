using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Dexla.Common.BlobStorage.Contracts;
using Dexla.Common.Types;
using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.BlobStorage;

public class BlobStorageService : IStorageService<BlobStorageModel>
{
    private readonly BlobContainerClient _containerClient;

    public BlobStorageService(string connectionString, string container)
    {
        _containerClient = new BlobContainerClient(connectionString, container);
        EnsureContainerCreated().Wait();
    }

    public async Task<IResponse> Upload(BlobStorageModel blobStorage)
    {
        try
        {
            BlobClient blobClient = GetBlobClient(blobStorage.Name);
            BlobHttpHeaders blobHttpHeader = new()
            {
                ContentType = blobStorage.ContentType
            };
            
            await blobClient.UploadAsync(blobStorage.Data, blobHttpHeader);
            
            string blobUrl = blobClient.Uri.ToString();
            
            return new UploadResponse(blobUrl);
        }
        catch (Exception e)
        {
            return new ErrorResponse(e.Message, nameof(blobStorage));
        }
    }

    public async ValueTask<(Stream, string?)> DownloadToStream(string name)
    {
        BlobClient blobClient = GetBlobClient(name);

        if (!await blobClient.ExistsAsync())
            return (Stream.Null, null);

        Stream? stream = await blobClient.OpenReadAsync();
        Response<BlobProperties>? properties = await blobClient.GetPropertiesAsync();
        return (stream, properties.Value.ContentType);
    }
    
    public async Task<IEnumerable<string>> SearchBlobsAsync(string searchString)
    {
        List<string> blobUrls = new();
    
        await foreach (BlobItem blobItem in _containerClient.GetBlobsAsync())
        {
            if (!blobItem.Name.Contains(searchString)) continue;
            BlobClient blobClient = GetBlobClient(blobItem.Name);
            blobUrls.Add(blobClient.Uri.ToString());
        }

        return blobUrls;
    }
    
    public async Task<string?> GetBlobUrlByName(string blobName)
    {
        BlobClient blobClient = GetBlobClient(blobName);

        if (await blobClient.ExistsAsync())
            return blobClient.Uri.ToString();

        return null;
    }

    public Task Delete(string name)
    {
        BlobClient blobClient = GetBlobClient(name);
        return blobClient.DeleteAsync();
    }

    private BlobClient GetBlobClient(string blobName)
    {
        return _containerClient.GetBlobClient(blobName);
    }

    private async Task EnsureContainerCreated()
    {
        if (!await _containerClient.ExistsAsync())
        {
            await _containerClient.CreateAsync();
        }
    }
}