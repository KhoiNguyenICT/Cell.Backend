using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cell.Core.RestClient
{
    public interface IRestClient
    {
        void SetBaseAddress(string uri);
        void AddDefaultHeader(string key, string value);
        Task<HttpResponseMessage> RequestAsync(string url, HttpMethod method = null, object data = null);
        Task<T> RequestAsync<T>(string url, HttpMethod method = null, object data = null) where T : class;
        Task<T> GetAsync<T>(string url) where T : class;
        Task<string> GetAsStringAsync(string url);
        Task<T> PostAsync<T>(string url, object data = null) where T : class;
        Task<T> PutAsync<T>(string url, object data) where T : class;
        Task<T> DeleteAsync<T>(string url) where T : class;
        Task<HttpResponseMessage> DeleteAsync(string url);
        Task<T> PostFormDataAsync<T>(string url, MultipartFormDataContent formData) where T : class;
    }
}
