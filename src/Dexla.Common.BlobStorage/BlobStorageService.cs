using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Dexla.Common.BlobStorage.Contracts;
using Dexla.Common.Types;
using Dexla.Common.Types.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace Dexla.Common.BlobStorage;

public class BlobStorageService : IStorageService<BlobStorageModel>
{
    private readonly BlobContainerClient _containerClient;
    private const int ChunkSize = 4 * 1024 * 1024; // 4 MB chunk size
    
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

             Stream uploadStream;
            if (blobStorage.ContentType.StartsWith("image"))
            {
                uploadStream = await CompressImage(blobStorage.Data, blobStorage.CompressionLevel);
            }
            else
            {
                uploadStream = blobStorage.Data;
            }

            long streamLength = uploadStream.Length;
            uploadStream.Position = 0;

            if (streamLength >= ChunkSize)
            {
                await UploadStreamInChunks(blobClient, uploadStream, blobHttpHeader);
            }
            else
            {
                await blobClient.UploadAsync(uploadStream, blobHttpHeader);
            }
            
            string blobUrl = blobClient.Uri.ToString();

            return new BlobResponse(blobUrl);
        }
        catch (Exception e)
        {
            return new ErrorResponse(e.Message, nameof(blobStorage));
        }
    }
    
    private async Task UploadStreamInChunks(BlobClient blobClient, Stream stream, BlobHttpHeaders headers)
    {
        stream.Position = 0;
        long streamLength = stream.Length;

        // Create BlockBlobClient using the same approach as BlobContainerClient
        BlockBlobClient blockBlobClient = _containerClient.GetBlockBlobClient(blobClient.Name);
        List<string> blockList = [];

        int blockId = 0;
        for (int i = 0; i < streamLength; i += ChunkSize)
        {
            byte[] buffer = new byte[Math.Min(ChunkSize, streamLength - i)];
            int bytesRead = await stream.ReadAsync(buffer);

            string blockIdString = Convert.ToBase64String(BitConverter.GetBytes(blockId));
            using (MemoryStream ms = new(buffer, 0, bytesRead))
            {
                await blockBlobClient.StageBlockAsync(blockIdString, ms);
            }
            blockList.Add(blockIdString);
            blockId++;
        }

        await blockBlobClient.CommitBlockListAsync(blockList, headers);
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

    public async Task<ListBlobResponse> SearchBlobsAsync(string searchString)
    {
        List<string> blobUrls = new();

        await foreach (BlobItem blobItem in _containerClient.GetBlobsAsync())
        {
            if (!blobItem.Name.Contains(searchString)) continue;
            BlobClient blobClient = GetBlobClient(blobItem.Name);
            blobUrls.Add(blobClient.Uri.ToString());
        }

        return new ListBlobResponse(blobUrls);
    }

    public async Task<BlobResponse?> GetBlobUrlByName(string blobName)
    {
        BlobClient blobClient = GetBlobClient(blobName);

        if (!await blobClient.ExistsAsync()) return null;
        string blobUrl = blobClient.Uri.ToString();

        return new BlobResponse(blobUrl);
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

    private async Task<Stream> CompressImage(Stream inputStream, int quality = 60)
    {
        inputStream.Position = 0;
        using Image image = await Image.LoadAsync(inputStream);
        JpegEncoder jpegEncoder = new()
        {
            Quality = quality
        };

        MemoryStream compressedStream = new();
        await image.SaveAsync(compressedStream, jpegEncoder);
        compressedStream.Position = 0;
        return compressedStream;
    }
}