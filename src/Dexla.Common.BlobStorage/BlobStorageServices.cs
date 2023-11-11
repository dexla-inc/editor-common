using Dexla.Common.BlobStorage.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Dexla.Common.BlobStorage;

public static class BlobStorageServices
{
    public static IServiceCollection AddBlobStorage(
        this IServiceCollection services,
        IConfiguration configuration)                                                                                                       
    {
        services
            .AddOptions()
            .Configure<BlobStorageSettings>(configuration.GetSection(nameof(BlobStorageSettings)))
            .AddSingleton<IStorageService<BlobStorageModel>, BlobStorageService>(serviceProvider =>
            {
                IOptions<BlobStorageSettings> blobStorageSettingsOptions =
                    serviceProvider.GetRequiredService<IOptions<BlobStorageSettings>>();

                if (blobStorageSettingsOptions.Value is null)
                    throw new Exception("Unable to find Blob Storage settings");
                    
                return new BlobStorageService(
                    blobStorageSettingsOptions.Value.ConnectionString,
                    blobStorageSettingsOptions.Value.Container);
            });

        return services;
    }
}