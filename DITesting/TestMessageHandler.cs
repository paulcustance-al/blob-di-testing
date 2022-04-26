using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DITesting;

// public class TestMessageHandler : DelegatingHandler
// {
//     private readonly Func<Task<string>> _getToken;
//
//     public TestMessageHandler(Func<Task<string>> getToken)
//     {
//         _getToken = getToken;
//     }
//     
//     protected override async Task<HttpResponseMessage> SendAsync(
//         HttpRequestMessage request,
//         CancellationToken cancellationToken)
//     {
//         var token = await _getToken();
//         return await base.SendAsync(request, cancellationToken);
//     }
// }

public class TestMessageHandler : DelegatingHandler
{
    private readonly IDelegateTokenRetrieval _tokenRetriever;

    public TestMessageHandler(IDelegateTokenRetrieval tokenRetriever)
    {
        _tokenRetriever = tokenRetriever;
    }
    
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var token = await _tokenRetriever.GetToken();
        return await base.SendAsync(request, cancellationToken);
    }
}

public interface IDelegateTokenRetrieval
{
    Task<string> GetToken();
}

public class DefaultDelegateTokenRetrieval : IDelegateTokenRetrieval
{
    private readonly HttpClient _client;

    public DefaultDelegateTokenRetrieval(HttpClient client)
    {
        _client = client;
    }
    
    public async Task<string> GetToken()
    {
        return await Task.FromResult("WoooGotAToken");
    }
}

