﻿using ReadLog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using System.Diagnostics;
using ReadLog.MVVM.Models;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ReadLog.Services
{

    public interface IDataService<TObject>
    {
        string _dataBasePath { get; set; }
        void setDataBasePath (string dataBasePath);
        Task<List<TObject>> LoadDataAsync();
        Task AddDataAsync(TObject manga);
        Task UpdateMangaAsync(TObject manga);
        Task<ImageSource> LoadImageAsync(TObject manga);
        Task<string> GetMangaTitleAsync(TObject manga);
    }
    public class DataService<TObject> : Observable, IDataService<TObject>
    {
        public string _dataBasePath { get; set; }

        public async Task AddDataAsync(TObject manga)
        {
            List<TObject> data = await LoadDataAsync();
            if (!string.IsNullOrEmpty(_dataBasePath))
            {
                data.Add(manga);
                var updatedJson = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(_dataBasePath, updatedJson);
            }

        }

        public async Task<string> GetMangaTitleAsync(TObject manga)
        {
            List<TObject> result = await LoadDataAsync();
            foreach (var item in result)
            {
                if (item is Manga storedManga && manga is Manga selectedManga && storedManga.Name == selectedManga.Name)
                {
                    return storedManga.Name;
                }
            }
            return "Not found";
        }

        public async Task<List<TObject>> LoadDataAsync()
        {

            if (!File.Exists(_dataBasePath) || new FileInfo(_dataBasePath).Length == 0)
            {
                using (FileStream fs = File.Create(_dataBasePath)) { };
                return new List<TObject>();
            }

            else
            {

                try
                {
                    var json = await File.ReadAllTextAsync(_dataBasePath);
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    };

                    var result = JsonSerializer.Deserialize<List<TObject>>(json, options);

                    Debug.WriteLine("SUCCESS JSON LOADING");

                    return result ?? new List<TObject>();

                }
                catch (JsonException ex)
                {
                    Debug.WriteLine("JSON_ERROR ", ex.Message);
                    throw new CustomJsonErrorException();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("ERROR ", ex.Message);
                    return new List<TObject>();
                }
            }
        }

        public async Task<ImageSource> LoadImageAsync(TObject manga)
        {
            try
            {
                List<TObject> result = await LoadDataAsync();
                string imagePath = null;

                foreach (var item in result)
                {
                    if (item is Manga storedManga && manga is Manga selectedManga && storedManga.Name == selectedManga.Name)
                    {
                        imagePath = storedManga?.CoverArt_Path; break;
                    }
                }

                if (imagePath != null && !string.IsNullOrEmpty(imagePath))
                {
                    try
                    {
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.UriSource = new Uri(imagePath, UriKind.RelativeOrAbsolute);
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.EndInit();
                        bitmap.Freeze();

                        Debug.WriteLine("Image Chargée");

                        return bitmap;
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                        return null;
                    }
                }
                return null;
            }
            catch (JsonException ex) { Debug.WriteLine("JSON_ERROR ", ex.Message); return null; }


        }

        public void setDataBasePath(string dataBasePath)
        {
            _dataBasePath = dataBasePath;
        }

        public async Task UpdateMangaAsync(TObject manga)
        {
            try
            {
                List<TObject> result = await LoadDataAsync();
                foreach (var item in result)
                {
                    if (item is Manga storedManga && manga is Manga selectedManga && storedManga.Name == selectedManga.Name)
                    {
                        storedManga.IsFavorite = selectedManga.IsFavorite; break;
                    }
                }
                var updatedJson = JsonSerializer.Serialize(result, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(_dataBasePath, updatedJson);
            }
            catch (JsonException ex) { Debug.WriteLine("JSON_ERROR ", ex.Message); }
        }
    }
}
