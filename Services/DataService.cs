using ReadLog.Core;
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

namespace ReadLog.Services
{

    public interface IDataService<TObject>
    {
        string filePath { get; set; }
        Task<List<TObject>> LoadDataAsync();
        Task AddDataAsync(TObject manga);
    }
    public class DataService<TObject> : Observable, IDataService<TObject>
    {
        public string filePath { get; set; }

        public async Task AddDataAsync(TObject manga)
        {
            List<TObject> data = await LoadDataAsync();
            if(!string.IsNullOrEmpty(filePath))
            {
                data.Add(manga);
                var updatedJson = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true});
                await File.WriteAllTextAsync(filePath, updatedJson);
            }

        }

        public async Task<List<TObject>> LoadDataAsync()
        {

            if (!File.Exists(filePath))
            {
                return new List<TObject>();
            }


            try
            {
                var json = await File.ReadAllTextAsync(filePath);
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
                Debug.WriteLine("ERROR ", ex.Message);
                return new List<TObject>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERROR ", ex.Message);
                return new List<TObject>();
            }
        }

    }
}
