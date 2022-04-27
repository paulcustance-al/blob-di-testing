using System;
using System.Net.Http;
using EHR.BlobStorage.Sdk.V1;
using Microsoft.Extensions.DependencyInjection;

namespace DiTestingLibrary
{
    public static class ServiceCollectionExtensions
    {
        public static void AddBlobStorage<T>(
            this IServiceCollection services,
            BlobStorageOptions userOptions)
            where T : class, IDelegateTokenRetrieval
        {
            services.AddOptions<BlobStorageOptions>().Configure(options =>
            {
                options.Name = userOptions.Name;
                options.BaseAddress = userOptions.BaseAddress;
                options.SslProtocol = options.SslProtocol;
            });
            
            services.AddBlobStorageClient<T>();
        }
        
        public static void AddBlobStorage<T>(
            this IServiceCollection services,
            Action<BlobStorageOptions> setupAction)
            where T : class, IDelegateTokenRetrieval
        {
            services.Configure(setupAction);
            services.AddBlobStorageClient<T>();
        }

        private static void AddBlobStorageClient<T>(this IServiceCollection services) 
            where T : class, IDelegateTokenRetrieval
        {
            services.AddTransient<TestMessageHandler>();
            services.AddTransient<IDelegateTokenRetrieval, T>();
            
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
        }
    }
}