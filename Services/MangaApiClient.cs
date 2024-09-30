using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ReadLog.Services
{
    public interface IMangaApiClient
    {
        Task<HttpStatusCode> GetAPICode();
        Task<bool> GetAPIStatus();
    }
    public class MangaApiClient : IMangaApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly List<string> _api = new List<string> { "https://api.mangadex.org/manga" };

        public MangaApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<HttpStatusCode> GetAPICode()
        {
            var response = await _httpClient.GetAsync(_api[0]);
            return response.StatusCode;
        }

        public async Task<bool> GetAPIStatus()
        {
            HttpStatusCode statusCode = await GetAPICode();
            return (statusCode == HttpStatusCode.OK);
        }
    }
}
