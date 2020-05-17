using System.Net.Http;

namespace ClientAPI.Services
{
    public interface IAPIService
    {
        HttpClient Client { get; }
    }
}