using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DiTestingLibrary
{
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
}