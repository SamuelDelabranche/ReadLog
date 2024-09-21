using ReadLog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

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
            if (File.Exists(filePath))
            {
                var json = await File.ReadAllTextAsync(filePath);
                return JsonSerializer.Deserialize<List<TObject>>(json);
            }return new List<TObject>();
        }
    }
}
