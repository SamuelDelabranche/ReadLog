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

        public event Action<TObject> itemAdded;

        public DataStore(IDataService<TObject> dataService)
        {
            _dataService = dataService;
            Items = new ObservableCollection<TObject>();

        }

        public async Task LoadDataAsync()
        {
            var items = await _dataService.LoadDataAsync();
            Items.Clear();
            foreach (var item in items)
            {
                Items.Add(item);
            }

        }

        public async Task AddDataAsync(TObject manga)
        {
            bool alreadyAdded = false;
            foreach (var item in Items)
            {
                if (item is Manga mangaStored && (Manga)(object)manga == mangaStored)
                {
                    alreadyAdded = true;
                    break;
                }
            }

            if (!alreadyAdded)
            {
                itemAdded?.Invoke(manga);
                Items.Add(manga);
                await _dataService.AddDataAsync(manga);
            }

        }
    }
}
