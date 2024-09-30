using ReadLog.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace ReadLog.Services
{
    public interface IMangaApiClient
    {
        Task<HttpStatusCode> GetAPICode();
        Task<Manga> GetMangaByName(string mangaName);
        Task<bool> GetAPIStatus();
        Task<bool> isMangaExist(string mangaName);
    }
    public class MangaApiClient : IMangaApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly List<string> _api = new List<string> { "https://api.mangadex.org/manga" };

        public MangaApiClient()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("ReadLog", "0.1"));
        }


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
        public async Task<Manga> GetMangaByName(string mangaName)
        {
            if (string.IsNullOrEmpty(mangaName) || !await isMangaExist(mangaName))
            {
                return null;
            }
            var reponse = await _httpClient.GetAsync($"{_api[0]}?title={Uri.EscapeDataString(mangaName)}");

            var jsonResponse = await reponse.Content.ReadAsStringAsync();
            var mangaData = JsonDocument.Parse(jsonResponse).RootElement.GetProperty("data");

            foreach (var manga in mangaData.EnumerateArray())
            {
                if (manga.GetProperty("attributes").GetProperty("title").GetProperty("en").GetString().ToLower() == mangaName.ToLower())
                {
                    return new Manga
                    {
                        ID = manga.GetProperty("id").ToString(),
                        Description = manga.GetProperty("attributes").TryGetProperty("description", out var descriptionProperty)
                                    ? descriptionProperty.GetString()
                                    : "Not found",
                        CoverArt_ID = manga.GetProperty("attributes").GetProperty("cover").GetString(),
                        CoverArt_Path = $"{_api[0]}/cover/{manga.GetProperty("attributes").GetProperty("cover").GetString()}",
                    };
                };
            }

            return null;
        }

        public async Task<bool> isMangaExist(string mangaName)
        {
            var reponse = await _httpClient.GetAsync($"{_api[0]}?title={Uri.EscapeDataString(mangaName)}");

            var jsonResponse = await reponse.Content.ReadAsStringAsync();
            var mangaData = JsonDocument.Parse(jsonResponse).RootElement.GetProperty("data");

            foreach (var manga in mangaData.EnumerateArray())
            {

                if (manga.GetProperty("attributes").GetProperty("title").GetProperty("en").GetString().ToLower() == mangaName.ToLower()) return true;
            }
            return false;
        }
    }
}
