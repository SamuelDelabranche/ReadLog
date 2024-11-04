using Microsoft.Extensions.DependencyInjection;
using ReadLog.Core;
using ReadLog.MVVM.Models;
using ReadLog.MVVM.ViewModels;
using ReadLog.MVVM.Views;
using ReadLog.Services;
using ReadLog.Stores;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows;

namespace ReadLog
{
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly IServiceCollection _services = new ServiceCollection();

        public App()
        {
            _services.AddSingleton(provider =>
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var appConfigSection = config.GetSection("AppConfig") as AppConfig;

                if (appConfigSection == null)
                {
                    appConfigSection = new AppConfig();
                }

                return appConfigSection;
            });

            _services.AddSingleton<IDataService<Manga>, DataService<Manga>>();
            _services.AddSingleton(provider =>
            {
                var dataService = provider.GetRequiredService<IDataService<Manga>>();
                var appConfig = provider.GetRequiredService<AppConfig>();

                var dataStore = new DataStore<Manga>(dataService);

                dataStore.setDataBasePath(appConfig.DataFilePath);

                return dataStore;
            });

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
                DataContext = provider.GetRequiredService<MainViewModel>()
            });

            _services.AddSingleton<AddMangaWindow>(provider => new AddMangaWindow()
            {
                DataContext = provider.GetRequiredService<AddMangaViewModel>()
            });

            _serviceProvider = _services.BuildServiceProvider();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (config.Sections["AppConfig"] == null)
            {
                var appConfigSection = _serviceProvider.GetRequiredService<AppConfig>();
                config.Sections.Add("AppConfig", appConfigSection);
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("AppConfig");
            }

            await InitializeDataStoreAsync();
            _serviceProvider.GetRequiredService<MainWindow>().Show();
            base.OnStartup(e);
        }

        private async Task InitializeDataStoreAsync()
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
