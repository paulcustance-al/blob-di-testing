namespace DITesting;

public interface IDelegateTokenRetrieval
{
    Task<string> GetToken();
}