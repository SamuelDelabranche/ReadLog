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


        private string _APIStatus;
        public string APIStatus
        {
            get
            {
                return _APIStatus;
            }
            set
            {
                _APIStatus = value;
                OnPropertyChanged(nameof(APIStatus));
            }
        }
        public HomeViewModel(INavigationService navigationService, DataStore<Manga> dataStore, IMangaApiClient mangaApiClient) : base(navigationService, dataStore)
        {
            _mangaApiClient = mangaApiClient;
            InitializeAsync();
        }

        private async void InitializeAsync()
        {
            bool status = await getAPIStatus();
            if (status) { APIStatus = "Ok"; } else { APIStatus = "Not Ok"; }
        }

        private async Task<bool> getAPIStatus()
        {
            var status = await _mangaApiClient.isMangaExist("Chainsaw Man");
            return status;

        }
    }
}
