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
    public class MainViewModel : ViewModelBase
    {

        private string _textBoxSearch;
        public string textBoxSearch
        {
            get
            {
                return _textBoxSearch;
            }
            set
            {
                _textBoxSearch = value;
                _listViewFilterService.SetFilterText(_textBoxSearch);
                OnPropertyChanged(nameof(textBoxSearch));
            }
        }

        private bool _isTextBoxVisible;
        public bool isTextBoxVisible
        {
            get
            {
                return _isTextBoxVisible;
            }
            set
            {
                _isTextBoxVisible = value;
                OnPropertyChanged();
            }
        }
        public RelayCommand NavigationToLivre { get; }
        public RelayCommand NavigationToSettings { get; }
        public RelayCommand NavigationToManga { get; }
        public RelayCommand NavigationToHome { get; }
        public INavigationService Navigation { get => _navigationService; }

        private readonly IListViewFilterService _listViewFilterService;

        public MainViewModel(INavigationService navigationService, DataStore<Manga> dataStore, IListViewFilterService listViewFilterService) : base(navigationService, dataStore)
        {

            _isTextBoxVisible = false;
            _listViewFilterService = listViewFilterService;

            _navigationService.NavigateTo<HomeViewModel>();

            NavigationToHome = new RelayCommand(execute => NavigateToPage(0));
            NavigationToManga = new RelayCommand(execute => NavigateToPage(1));
            NavigationToSettings = new RelayCommand(execute => NavigateToPage(2));
        }

        private void NavigateToPage(int page)
        {
            switch (page)
            {
                case 0:
                    _navigationService.NavigateTo<HomeViewModel>();
                    isTextBoxVisible = false; break;

                case 1:
                    _navigationService.NavigateTo<MangaViewModel>();
                    isTextBoxVisible = true; break;

                case 2:
                    _navigationService.NavigateTo<SettingsViewModel>();
                    isTextBoxVisible = false; break;
            }
        }

    }
}
