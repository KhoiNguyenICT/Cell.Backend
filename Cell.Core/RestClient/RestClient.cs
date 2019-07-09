using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Polly;

namespace Cell.Core.RestClient
{
    public class RestClient : IRestClient, IDisposable
    {
        private readonly HttpClient _client;
        private readonly ILogger<RestClient> _logger;
        private static readonly HttpStatusCode[] RetryStatuses =
        {
            HttpStatusCode.BadGateway,
            HttpStatusCode.GatewayTimeout,
            HttpStatusCode.InternalServerError
        };

        public RestClient(ILogger<RestClient> logger)
        {
            _logger = logger;
            _client = new HttpClient();
        }

        public void SetBaseAddress(string uri)
        {
            _client.BaseAddress = new Uri(uri);
        }

        public void AddDefaultHeader(string key, string value)
        {
            _client.DefaultRequestHeaders.Add(key, value);
        }

        public async Task<HttpResponseMessage> RequestAsync(string url, HttpMethod method = null, object data = null)
        {
            var policy = Policy.Handle<HttpRequestException>()
                .Or<OperationCanceledException>()
                .OrResult<HttpResponseMessage>(res => RetryStatuses.Contains(res.StatusCode))
                .WaitAndRetryAsync(3, (attemp) => TimeSpan.FromSeconds(Math.Pow(2, attemp)), (ex, time) =>
                {
                    _logger.LogWarning("Error request {0}. Retry after {1} seconds", url, time, ex);
                });
            method = method ?? HttpMethod.Get;

            return await policy.ExecuteAsync(async () =>
            {
                var request = new HttpRequestMessage(method, url);
                switch (data)
                {
                    case FormUrlEncodedContent formUrlEncodedContent:
                        request.Content = formUrlEncodedContent;
                        break;

                    default:
                        var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8,
                            "application/json");
                        request.Content = content;
                        break;
                }

                var response = await _client.SendAsync(request);
                return response;
            });
        }

        public async Task<T> RequestAsync<T>(string url, HttpMethod method = null, object data = null) where T : class
        {
            var response = await RequestAsync(url, method, data);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }

        public async Task<T> GetAsync<T>(string url) where T : class
        {
            return await RequestAsync<T>(url);
        }

        public async Task<T> PostAsync<T>(string url, object data = null) where T : class
        {
            return await RequestAsync<T>(url, HttpMethod.Post, data);
        }

        public async Task<T> PutAsync<T>(string url, object data) where T : class
        {
            return await RequestAsync<T>(url, HttpMethod.Put, data);
        }

        public async Task<T> DeleteAsync<T>(string url) where T : class
        {
            return await RequestAsync<T>(url, HttpMethod.Delete);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string url)
        {
            return await RequestAsync(url, HttpMethod.Delete);
        }

        public async Task<T> PostFormDataAsync<T>(string url, MultipartFormDataContent formData) where T : class
        {
            var response = await _client.PostAsync(url, formData);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }

        public async Task<string> GetAsStringAsync(string url)
        {
            var response = await RequestAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        #region IDisposable Support
        private bool _disposedValue; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue) return;
            if (disposing)
            {
                _client?.Dispose();
            }


            _disposedValue = true;
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~RestClient() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
