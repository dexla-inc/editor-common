using Dexla.Common.BlobStorage.Contracts;
using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.BlobStorage;

public interface IStorageService<in T> where T : class, IStorageModel
{
    Task<IResponse> Upload(BlobStorageModel blobStorage);
    ValueTask<(Stream, string?)> DownloadToStream(string name);
    Task<BlobResponse?> GetBlobUrlByName(string blobName);
    Task<ListBlobResponse> SearchBlobsAsync(string searchString);
    Task Delete(string name);
}