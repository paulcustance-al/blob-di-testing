namespace DITesting;

public class TestMessageHandler : DelegatingHandler
{
    private readonly Func<Task<string>> _getToken;

    public TestMessageHandler(Func<Task<string>> getToken)
    {
        _getToken = getToken;
    }
    
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var token = await _getToken();
        return await base.SendAsync(request, cancellationToken);
    }
}