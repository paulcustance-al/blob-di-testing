using System.Net.Http;
using System.Threading.Tasks;

namespace DiTestingLibrary
{
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
}