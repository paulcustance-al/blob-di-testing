using EHR.BlobStorage.Sdk.V1;
using Microsoft.Extensions.Options;

namespace DITesting;

public static class ServiceCollectionExtensions
{
    public static BlobStorageBuilder AddBlobStorage(this IServiceCollection services, Action<BlobStorageOptions>? setupAction = null)
    {
        if (setupAction != null) services.ConfigureBlobStorage(setupAction);

        services
            .AddHttpClient<IBlobStorageClient, BlobStorageClient>((client, provider) =>
            {
                var options = provider.GetRequiredService<BlobStorageOptions>();
                client.BaseAddress = new Uri(options.BaseAddress);
                
                return new BlobStorageClient(client, StorageClientType.Azure);
            })
            .AddHttpMessageHandler<TestMessageHandler>()
            .ConfigurePrimaryHttpMessageHandler(provider =>
            {
                var options = provider.GetRequiredService<BlobStorageOptions>();
                
                return new HttpClientHandler
                {
                    SslProtocols = options.SslProtocol
                };
            });

        return new BlobStorageBuilder(services);
    }

    private static void ConfigureBlobStorage(this IServiceCollection services, Action<BlobStorageOptions> setupAction)
    {
        services
            .Configure(setupAction)
            .AddSingleton(resolver => resolver.GetRequiredService<IOptions<BlobStorageOptions>>().Value);
    }
}