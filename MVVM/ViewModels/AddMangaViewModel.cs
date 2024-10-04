using ReadLog.Core;
using ReadLog.MVVM.Models;
using ReadLog.Services;
using ReadLog.Stores;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ReadLog.MVVM.ViewModels
{
    public class AddMangaViewModel : ViewModelBase
    {

        public RelayCommand AddMangaCommand { get; set; }

        private string _mangaName;
        public string MangaName
        {
            get
            {
                return _mangaName;
            }
            set
            {
                _mangaName = value;
                OnPropertyChanged(nameof(MangaName));
            }
        }

        private int _numberChapiter;
        public int NumberChapiter
        {
            get
            {
                return _numberChapiter;
            }
            set
            {
                _numberChapiter = value;
                OnPropertyChanged(nameof(NumberChapiter));
            }
        }

        private bool _isFavorite;
        public bool IsFavorite
        {
            get
            {
                return _isFavorite;
            }
            set
            {
                _isFavorite = value;
                OnPropertyChanged(nameof(IsFavorite));
            }
        }

        private readonly IMangaApiClient _mangaApiClient;
        public AddMangaViewModel(INavigationService navigationService, DataStore<Manga> dataStore, IMangaApiClient mangaApiClient) : base(navigationService, dataStore)
        {
            _mangaApiClient = mangaApiClient;
            AddMangaCommand = new RelayCommand(async execute => await AddManga(), canExecute => checkBoxes());
        }

        private bool checkBoxes()
        {
            return !string.IsNullOrEmpty(_mangaName);
        }

        public void resetUI()
        {
            IsFavorite = false;
            MangaName = "";
            NumberChapiter = 0;
        }

        private async Task AddManga()
        {
            Manga newManga = await _mangaApiClient.GetMangaByName(_mangaName);
            if (newManga != null)
            {
                Debug.WriteLine(newManga.ToString());
                await _datatStore.AddDataAsync(newManga);

            }

        }
    }
}
