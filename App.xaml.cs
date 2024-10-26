﻿using Microsoft.Extensions.DependencyInjection;
using ReadLog.Core;
using ReadLog.MVVM.Models;
using ReadLog.MVVM.ViewModels;
using ReadLog.MVVM.Views;
using ReadLog.Services;
using ReadLog.Stores;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;

namespace ReadLog
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly IServiceCollection _services = new ServiceCollection();
        public App()
        {
            _services.AddSingleton<IDataService<Manga>, DataService<Manga>>();
            _services.AddSingleton<DataStore<Manga>>();

            _services.AddSingleton<MainViewModel>();
            _services.AddSingleton<AddMangaViewModel>();

            _services.AddSingleton<HomeViewModel>();
            _services.AddSingleton<MangaViewModel>();
            _services.AddSingleton<SettingsViewModel>();
            _services.AddSingleton<MangaEditionViewModel>();

            _services.AddSingleton<INavigationService, NavigationService>();
            _services.AddSingleton<IViewModelFactory, ViewModelFactory>();
            _services.AddSingleton<IListViewFilterService, ListViewFilterService>();
            _services.AddSingleton<IMangaApiClient, MangaApiClient>();

            _services.AddSingleton<MainWindow>(provider => new MainWindow()
            {
                DataContext = _serviceProvider.GetRequiredService<MainViewModel>()
            });

            _services.AddSingleton<AddMangaWindow>(provider => new AddMangaWindow()
            {
                DataContext = _serviceProvider.GetRequiredService<AddMangaViewModel>()
            });

            _serviceProvider = _services.BuildServiceProvider();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            _serviceProvider.GetRequiredService<IDataService<Manga>>().filePath = "../../../Stores/Data.json";

            await InitializeAsync();

            _serviceProvider.GetRequiredService<MainWindow>().Show();
            base.OnStartup(e);
        }

        private async Task InitializeAsync()
        {
            DataStore<Manga> dataStore = _serviceProvider.GetRequiredService<DataStore<Manga>>();
            try
            {
                await dataStore.LoadDataAsync();
            }
            catch (CustomExceptionBase ex)
            {
                dataStore.Status.DisplayMessage(ex);
            }
        }
    }
}
