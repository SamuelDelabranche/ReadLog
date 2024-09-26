using ReadLog.MVVM.Models;
using ReadLog.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadLog.Stores
{
    public class DataStore<TObject>
    {
        private readonly IDataService<TObject> _dataService;
        public ObservableCollection<TObject> Items { get; set; }

        public DataStore(IDataService<TObject> dataService)
        {
            _dataService = dataService;
            Items = new ObservableCollection<TObject>();

        }

        public async Task LoadDataAsync(string filepath)
        {
            var items = await _dataService.LoadDataAsync(filepath);
            Items.Clear();
            foreach (var item in items)
            {
                Items.Add(item);
            }

        }
    }
}
