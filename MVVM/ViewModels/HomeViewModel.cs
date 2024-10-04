using ReadLog.Core;
using ReadLog.MVVM.Models;
using ReadLog.Services;
using ReadLog.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadLog.MVVM.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly IMangaApiClient _mangaApiClient;
        public HomeViewModel(INavigationService navigationService, DataStore<Manga> dataStore, IMangaApiClient mangaApiClient) : base(navigationService, dataStore)
        {
            _mangaApiClient = mangaApiClient;
        }

    }
}
