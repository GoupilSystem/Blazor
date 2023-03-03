using Birk.Client.Bestilling.Models.HttpResults;

namespace Birk.Client.Bestilling.Services.Interfaces
{
    public interface IHttpService
    {
        Task<HttpResult<T>> HttpGet<T>(string uri) where T : class;
        Task<HttpResult<T>> HttpDelete<T>(string uri, int id) where T : class;
        Task<HttpResult<T>> HttpPost<T>(string uri, object dataToSend) where T : class;
        Task<HttpResult<T>> HttpPut<T>(string uri, object dataToSend) where T : class;
    }
}
