using Microsoft.Extensions.DependencyInjection;
using ReadLog.MVVM.Models;
using ReadLog.MVVM.ViewModels;
using ReadLog.MVVM.Views;
using ReadLog.Services;
using ReadLog.Stores;
using System.Windows;

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
            _services.AddSingleton<AddMangaViewModel>();

            _services.AddSingleton<HomeViewModel>();
            _services.AddSingleton<MangaViewModel>();
            _services.AddSingleton<SettingsViewModel>();

            _services.AddSingleton<INavigationService,  NavigationService>();
            _services.AddSingleton<IViewModelFactory, ViewModelFactory>();
            _services.AddSingleton<IDataService<Manga>, DataService<Manga>>();
            _services.AddSingleton<IListViewFilterService, ListViewFilterService>();

            _services.AddSingleton<IMangaApiClient, MangaApiClient>();

            _services.AddSingleton<DataStore<Manga>>();

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

        protected override void OnStartup(StartupEventArgs e)
        {

            _serviceProvider.GetRequiredService<MainWindow>().Show();
            _serviceProvider.GetRequiredService<IDataService<Manga>>().filePath = "../../../Stores/Data.json";
            base.OnStartup(e);
        }
    }
}
