using Birk.Client.Bestilling.Models.Configuration;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text;
using Birk.Client.Bestilling.Models;

namespace Birk.Client.Bestilling.Services
{
    public class HttpService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;


        public HttpService(HttpClient httpClient, IOptions<BaseUrlConfiguration> baseUrlConfiguration)
        {
            _httpClient = httpClient;
            _apiUrl = baseUrlConfiguration.Value.ApiBase;
        }

        public async Task<T> HttpGet<T>(string uri)
            where T : class
        {
            var result = await _httpClient.GetAsync($"{_apiUrl}{uri}");
            if (!result.IsSuccessStatusCode)
            {
                return null;
            }

            return await FromHttpResponseMessage<T>(result);
        }

        public async Task<T> HttpDelete<T>(string uri, int id)
            where T : class
        {
            var result = await _httpClient.DeleteAsync($"{_apiUrl}{uri}/{id}");
            if (!result.IsSuccessStatusCode)
            {
                return null;
            }

            return await FromHttpResponseMessage<T>(result);
        }

        public async Task<T> HttpPost<T>(string uri, object dataToSend)
            where T : class
        {
            var content = ToJson(dataToSend);

            var result = await _httpClient.PostAsync($"{_apiUrl}{uri}", content);
            if (!result.IsSuccessStatusCode)
            {
                var exception = JsonSerializer.Deserialize<ErrorDetails>(await result.Content.ReadAsStringAsync(), new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return null;
            }

            return await FromHttpResponseMessage<T>(result);
        }

        public async Task<T> HttpPut<T>(string uri, object dataToSend)
            where T : class
        {
            var content = ToJson(dataToSend);

            var result = await _httpClient.PutAsync($"{_apiUrl}{uri}", content);
            if (!result.IsSuccessStatusCode)
            {
                return null;
            }

            return await FromHttpResponseMessage<T>(result);
        }

        private StringContent ToJson(object obj)
        {
            return new StringContent(JsonSerializer.Serialize(obj), Encoding.UTF8, "application/json");
        }

        private async Task<T> FromHttpResponseMessage<T>(HttpResponseMessage result)
        {
            return JsonSerializer.Deserialize<T>(await result.Content.ReadAsStringAsync(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}
