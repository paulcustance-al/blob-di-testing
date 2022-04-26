using System.Security.Authentication;

namespace DITesting;

public class BlobStorageOptions
{
    public string BaseAddress { get; set; } = "https://jsonplaceholder.typicode.com/todos/2";
    
    public SslProtocols SslProtocol { get; set; } = SslProtocols.Tls11;

    public string Name { get; set; }
}