using ReadLog.Core;
using ReadLog.MVVM.Models;
using ReadLog.Services;
using ReadLog.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadLog.MVVM.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public ObservableCollection<Manga> Items { get; }
        public MessageViewModel DataError { get; set; }
        public HomeViewModel(INavigationService navigationService, DataStore<Manga> dataStore) : base(navigationService, dataStore)
        {
            if (_dataStore.Status.HasMessage)
            {
                DataError = _dataStore.Status;
                Items = null;
            }
            else
            {
                Items = _dataStore.Items;
            }
        }

    }
}
