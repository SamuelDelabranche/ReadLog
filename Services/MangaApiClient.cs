using ReadLog.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
        private readonly List<string> _api = new List<string> { "https://api.mangadex.org", "https://api.mangadex.org/manga" };

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
            var reponse = await _httpClient.GetAsync($"{_api[1]}?title={Uri.EscapeDataString(mangaName)}");

            Debug.WriteLine($"{_api[1]}?title={Uri.EscapeDataString(mangaName)}");
            var jsonResponse = await reponse.Content.ReadAsStringAsync();
            var mangaData = JsonDocument.Parse(jsonResponse).RootElement.GetProperty("data");

            var options = new JsonSerializerOptions { WriteIndented = true };


            foreach (var manga in mangaData.EnumerateArray())
            {
                if (manga.GetProperty("attributes").GetProperty("title").GetProperty("en").GetString().ToLower() == mangaName.ToLower())
                {

                    string formattedJson = JsonSerializer.Serialize(manga, options);
                    Debug.WriteLine(formattedJson);

                    try
                    {

                        string covertArtId = "Not Found";
                        foreach (var relationship in manga.GetProperty("relationships").EnumerateArray())
                        {
                            if (relationship.GetProperty("type").GetString() == "cover_art") { covertArtId = relationship.GetProperty("id").GetString(); break; }
                        }

                        return new Manga
                        {
                            Name = manga.GetProperty("attributes").GetProperty("title").GetProperty("en").GetString(),
                            ID = manga.TryGetProperty("id", out var IDProperty) ? IDProperty.GetString() : "Not found",

                            Description = manga.GetProperty("attributes").TryGetProperty("description", out var descriptionProperty)
                                                    ? descriptionProperty.TryGetProperty("fr", out var FRDescriptionProperty)
                                                        ? FRDescriptionProperty.GetString()
                                                        : descriptionProperty.TryGetProperty("en", out var ENDescriptionProperty)
                                                            ? ENDescriptionProperty.GetString()
                                                            : "Not found"
                                                    : "Not found",
                            CoverArt_ID = covertArtId,
                            CoverArt_Path = await getCoverArtAsync(manga.TryGetProperty("id", out var IDProperty2) ? IDProperty2.GetString() : "Not found", covertArtId),
                            NombreChapitreTotaux = manga.GetProperty("attributes").TryGetProperty("lastChapter", out var lastChapterProperty) ? string.IsNullOrEmpty(lastChapterProperty.GetString()) ? "Not found" : lastChapterProperty.GetString() : "Not found"
                        };
                    }
                    catch (Exception ex)
                    {

                        Debug.WriteLine("ERROR add newManga: " + ex.Message);
                        return new Manga
                        {
                            Name = manga.GetProperty("attributes").GetProperty("title").GetProperty("en").GetString(),
                            ID = manga.TryGetProperty("id", out var IDProperty) ? IDProperty.GetString() : "Not found"
                        };
                    }
                };
            }

            return null;
        }

        private async Task<string> getCoverArtAsync(string mangaID, string coverID)
        {
            if (mangaID == "Not found") return "Not found";

            try
            {
                var response = await _httpClient.GetAsync($"{_api[0]}/cover/{coverID}");

                if (!response.IsSuccessStatusCode)
                {
                    return $"API call failed: {response.ReasonPhrase}";
                }

                var jsonResponse = await response.Content.ReadAsStringAsync();

                JsonElement rootElement;
                try
                {
                    rootElement = JsonDocument.Parse(jsonResponse).RootElement;
                }
                catch (JsonException jsonEx)
                {
                    return $"JSON parse error: {jsonEx.Message}";
                }

                if (!rootElement.TryGetProperty("data", out JsonElement dataElement))
                {
                    return "The 'data' key was not found in the JSON response.";
                }

                if (!dataElement.TryGetProperty("attributes", out JsonElement attributesElement))
                {
                    return "The 'attributes' key was not found in the 'data' section.";
                }

                string coverFileName = attributesElement.TryGetProperty("fileName", out var fileName)
                                       ? fileName.GetString()
                                       : "Not found";

                if (coverFileName == "Not found")
                {
                    return "Cover fileName not found";
                }

                try
                {
                    byte[] imageBytes = await _httpClient.GetByteArrayAsync($"https://uploads.mangadex.org/covers/{mangaID}/{coverFileName}");
                    string filePath = Path.Combine("../../../Stores/covertArt/", coverFileName);
                    await File.WriteAllBytesAsync(filePath, imageBytes);

                    return filePath;
                }
                catch (HttpRequestException ex)
                {
                    return $"Image download error: {ex.Message}";
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }


        public async Task<bool> isMangaExist(string mangaName)
        {
            try
            {
                var reponse = await _httpClient.GetAsync($"{_api[1]}?title={Uri.EscapeDataString(mangaName)}");

                var jsonResponse = await reponse.Content.ReadAsStringAsync();
                var mangaData = JsonDocument.Parse(jsonResponse).RootElement.GetProperty("data");

                foreach (var manga in mangaData.EnumerateArray())
                {
                    if (manga.GetProperty("attributes").GetProperty("title").GetProperty("en").GetString().ToLower() == mangaName.ToLower()) return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
