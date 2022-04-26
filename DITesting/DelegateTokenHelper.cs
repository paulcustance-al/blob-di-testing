using System.Net.Http;
using System.Threading.Tasks;

namespace DITesting;

public class DelegatedTokenHelper
{
    private readonly HttpClient _client;

    public DelegatedTokenHelper(HttpClient client)
    {
        _client = client;
    }

    public async Task<string> GetToken()
    {
        var result = await _client.GetAsync("https://jsonplaceholder.typicode.com/todos/1");
        await Task.CompletedTask;
        return "MyAccessToken";
    }
}