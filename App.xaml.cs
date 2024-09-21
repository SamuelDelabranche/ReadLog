﻿using Microsoft.Extensions.DependencyInjection;
using ReadLog.Core;
using ReadLog.MVVM.Models;
using ReadLog.MVVM.ViewModels;
using ReadLog.Services;
using ReadLog.Stores;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Configuration;
using System.Data;
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
            _services.AddSingleton<MainViewModel>();
            _services.AddSingleton<HomeViewModel>();
            _services.AddSingleton<LivreViewModel>();
            _services.AddSingleton<MangaViewModel>();
            _services.AddSingleton<SettingsViewModel>();

            _services.AddSingleton<INavigationService,  NavigationService>();
            _services.AddSingleton<IViewModelFactory, ViewModelFactory>();
            _services.AddSingleton<IDataService<ItemBase>, DataService<ItemBase>>();
            _services.AddSingleton<DataStore<ItemBase>>();

            _services.AddSingleton<MainWindow>(provider => new MainWindow()
            {
                DataContext = _serviceProvider.GetRequiredService<MainViewModel>()
            });
            

            _serviceProvider = _services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {

            _serviceProvider.GetRequiredService<MainWindow>().Show();
            base.OnStartup(e);
        }
    }
}
