﻿using ReadLog.Core;
using ReadLog.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadLog.MVVM.ViewModels
{
    public class MainViewModel : ViewModelBase
    {

        public RelayCommand NavigationToLivre { get; }
        public RelayCommand NavigationToSettings { get; }
        public RelayCommand NavigationToManga { get; }
        public RelayCommand NavigationToHome { get; }
        public INavigationService Navigation { get => _navigationService; }

        public MainViewModel(INavigationService navigationService, IDataService<ItemBase> dataService) : base(navigationService, dataService)
        {
            _navigationService.NavigateTo<HomeViewModel>();

            NavigationToHome = new RelayCommand(execute => _navigationService.NavigateTo<HomeViewModel>());
            NavigationToLivre = new RelayCommand(execute => _navigationService.NavigateTo<LivreViewModel>());
            NavigationToManga = new RelayCommand(execute => _navigationService.NavigateTo<MangaViewModel>());
            NavigationToSettings = new RelayCommand(execute => _navigationService.NavigateTo<SettingsViewModel>());
        }


    }
}
