using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace DITesting;

// public interface IBlobStorageBuilder
// {
// }
//
// public class BlobStorageBuilder : IBlobStorageBuilder
// {
//     private readonly IServiceCollection _services;
//
//     public BlobStorageBuilder(IServiceCollection services)
//     {
//         _services = services;
//     }
//
//     public void ConfigureCustomTokenRetrieval(Func<IServiceProvider, Func<Task<string>>> tokenRetrieval)
//     {
//         _services.AddTransient(provider => new TestMessageHandler(tokenRetrieval(provider)));
//     }
// }
