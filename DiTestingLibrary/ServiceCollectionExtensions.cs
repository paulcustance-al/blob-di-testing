using System;
using System.Net.Http;
using EHR.BlobStorage.Sdk.V1;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DiTestingLibrary
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <typeparam name="T"></typeparam>
        public static void AddBlobStorage<T>(
            this IServiceCollection services,
            IConfiguration configuration)
            where T : class, IDelegateTokenRetrieval

        {
            services.AddTransient<TestMessageHandler>();
            services.AddTransient<IDelegateTokenRetrieval, T>();

            ConfigureBlobStorage(services, configuration);

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

        private static void ConfigureBlobStorage(IServiceCollection services, IConfiguration configuration)
        {
            services
                .Configure<BlobStorageOptions>(options => configuration.GetSection("BlobStorage").Bind(options))
                .AddSingleton(resolver => resolver.GetRequiredService<IOptions<BlobStorageOptions>>().Value);
        }
    }
}