namespace DITesting;

public interface IBlobStorageBuilder
{
}

public class BlobStorageBuilder : IBlobStorageBuilder
{
    private readonly IServiceCollection _services;

    public BlobStorageBuilder(IServiceCollection services)
    {
        _services = services;
    }

    public void ConfigureCustomTokenRetrieval(Func<IServiceProvider, Func<Task<string>>> tokenFactory)
    {
        _services.AddTransient(s => new TestMessageHandler(tokenFactory(s)));
    }
}
