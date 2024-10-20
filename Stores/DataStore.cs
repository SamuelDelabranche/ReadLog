using ReadLog.MVVM.Models;
using ReadLog.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

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

        public async Task UpdateMangaAsync(TObject manga)
        {
            if (manga is Manga selectedManga)
            {
                foreach (var item  in Items)
                {
                    if (item is Manga mangaStored && mangaStored.Name == selectedManga.Name)
                    {
                        mangaStored.IsFavorite = selectedManga.IsFavorite;
                    }
                }
            await _dataService.UpdateMangaAsync(manga);
            }
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

        public async Task<ImageSource> LoadImageAsync(TObject manga)
        {
            if (Items.Contains(manga))
            {
                return await _dataService.LoadImageAsync(manga);
            }

            return null;
        }
    }
}
