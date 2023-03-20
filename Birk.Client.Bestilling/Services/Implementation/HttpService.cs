using Birk.Client.Bestilling.Models.Configuration;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text;
using Birk.Client.Bestilling.Models.HttpResults;
using Birk.Client.Bestilling.Utils.Constants;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Birk.Client.Bestilling.Enums;
using System.Net.Http;
using Birk.Client.Bestilling.Services.Interfaces;

namespace Birk.Client.Bestilling.Services.Implementation
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService(HttpClient httpClient, string baseUrl, int httpTimeoutSeconds)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(baseUrl);
            _httpClient.Timeout = TimeSpan.FromSeconds(httpTimeoutSeconds);
        }

        public async Task<HttpResult<T>> HttpGet<T>(string uri)
            where T : class
        {
            try
            {
                var result = await _httpClient.GetAsync(uri);

                if (!result.IsSuccessStatusCode)
                {
                    return new HttpResult<T>(false, null, await GetProblemDetailsAsync(result, HttpProblemType.HttpGetNoSuccess));
                }

                var data = await FromHttpResponseMessage<T>(result);
                return new HttpResult<T>(true, data);
            }
            catch (HttpRequestException ex)
            {
                return new HttpResult<T>(false, null, await GetProblemDetailsAsync(null, HttpProblemType.HttpGetError, ex.Message));
            }
        }

        public async Task<HttpResult<T>> HttpDelete<T>(string uri, int id)
            where T : class
        {
            try
            {
                var result = await _httpClient.DeleteAsync($"{uri}/{id}");
                if (!result.IsSuccessStatusCode)
                {
                    return new HttpResult<T>(false, null, await GetProblemDetailsAsync(result, HttpProblemType.HttpDeleteNoSuccess));
                }

                var data = await FromHttpResponseMessage<T>(result);
                return new HttpResult<T>(true, data);
            }
            catch (HttpRequestException ex)
            {
                return new HttpResult<T>(false, null, await GetProblemDetailsAsync(null, HttpProblemType.HttpDeleteError, ex.Message));
            }

        }

        public async Task<HttpResult<T>> HttpPost<T>(string uri, object dataToSend)
            where T : class
        {
            try
            {
                var content = ToJson(dataToSend);

                var result = await _httpClient.PostAsync(uri, content);

                if (!result.IsSuccessStatusCode)
                {
                    return new HttpResult<T>(false, null, await GetProblemDetailsAsync(result, HttpProblemType.HttpPostNoSuccess));
                }

                var data = await FromHttpResponseMessage<T>(result);
                return new HttpResult<T>(true, data);
            }
            catch (HttpRequestException ex)
            {
                return new HttpResult<T>(false, null, await GetProblemDetailsAsync(null, HttpProblemType.HttpPostError, ex.Message));
            }
        }

        public async Task<HttpResult<T>> HttpPut<T>(string uri, object dataToSend)
            where T : class
        {
            try
            {
                var content = ToJson(dataToSend);

                var result = await _httpClient.PutAsync(uri, content);

                if (!result.IsSuccessStatusCode)
                {
                    return new HttpResult<T>(false, null, await GetProblemDetailsAsync(result, HttpProblemType.HttpPutNoSuccess));
                }

                var data = await FromHttpResponseMessage<T>(result);
                return new HttpResult<T>(true, data);
            }
            catch (HttpRequestException ex)
            {
                return new HttpResult<T>(false, null, await GetProblemDetailsAsync(null, HttpProblemType.HttpPuttError, ex.Message));
            }
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

        private async Task<ProblemDetails> GetProblemDetailsAsync(HttpResponseMessage? result,
            HttpProblemType httpErrorType, string detail = "")
        {
            var pDetail = "";

            if (detail != "") { pDetail = detail; }
            else if (result != null &&
                (httpErrorType == HttpProblemType.HttpGetNoSuccess || httpErrorType == HttpProblemType.HttpPostNoSuccess))
            {
                pDetail = await result.Content.ReadAsStringAsync();
            }

            var problemDetails = new ProblemDetails
            {
                Title = string.Format(Language.NO["HttpProblemTitle"], "bestilling"),
                Detail = string.Format(Language.NO[$"HttpProblemDetail"], httpErrorType, pDetail),
                Status = result != null ? (int)result.StatusCode : (int)HttpStatusCode.InternalServerError
            };

            return problemDetails;
        }
    }
}
