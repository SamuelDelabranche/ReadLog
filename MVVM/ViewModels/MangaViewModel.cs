using ReadLog.Core;
using ReadLog.Services;
using ReadLog.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReadLog.MVVM.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ReadLog.MVVM.ViewModels
{
    public class MangaViewModel : ViewModelBase
    {
        public ObservableCollection<Manga> Items { get; }
        public MangaViewModel(INavigationService navigationService, DataStore<Manga> dataStore) : base(navigationService, dataStore)
        {
            Items = dataStore.Items;
        }
    }
}
