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

namespace ReadLog.Services
{

    public interface IDataService<TObject>
    {
        Task<List<TObject>> LoadDataAsync(string filePath);
    }
    public class DataService<TObject> : Observable, IDataService<TObject>
    {
        public async Task<List<TObject>> LoadDataAsync(string filePath)
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
