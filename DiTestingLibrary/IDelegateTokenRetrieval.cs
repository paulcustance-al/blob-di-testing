using System.Threading.Tasks;

namespace DiTestingLibrary
{
    public interface IDelegateTokenRetrieval
    {
        Task<string> GetToken();
    }
}