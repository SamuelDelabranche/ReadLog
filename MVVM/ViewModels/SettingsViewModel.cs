using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using ReadLog.Core;
using ReadLog.MVVM.Models;
using ReadLog.MVVM.Views;
using ReadLog.Services;
using ReadLog.Stores;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadLog.MVVM.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private string _dataPath;
        public string dataPath
        {
            get
            {
                return _dataPath;
            }
            set
            {
                _dataPath = value;
                OnPropertyChanged(nameof(dataPath));
            }
        }

        public RelayCommand SelectedData { get; }
        public RelayCommand SaveConfiguration { get; }

        private readonly AppConfig _appConfig;
        public SettingsViewModel(INavigationService navigationService, DataStore<Manga> dataStore, AppConfig appConfig) : base(navigationService, dataStore)
        {
            _appConfig = appConfig;
            try
            {
                dataPath = _appConfig.DataFilePath;
            }
            catch
            {
                dataPath = "Not Found";
            }

            SelectedData = new RelayCommand(execute => OpenFileSelector());
            SaveConfiguration = new RelayCommand(async execute => await SaveAppConfigurationAsync());
        }

        private async Task SaveAppConfigurationAsync()
        {
            try
            {
                System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);


                AppConfig appConfigurationSection = config.GetSection("AppConfig") as AppConfig;

                appConfigurationSection.DataFilePath = dataPath;

                appConfigurationSection.SectionInformation.ForceSave = true;
                config.Save(ConfigurationSaveMode.Modified);

                ConfigurationManager.RefreshSection("AppConfig");
            }
            catch (ConfigurationErrorsException err)
            {
                Console.WriteLine("SaveConfigurationFile: {0}", err.ToString());
                throw;
            }

            await _dataStore.LoadDataAsync();
        }

        private void OpenFileSelector()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON file (*.json)|*.json";

            string projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
            string initialDirectory = Path.Combine(projectDirectory, "Stores");

            if (Directory.Exists(initialDirectory))
                openFileDialog.InitialDirectory = initialDirectory;
            else
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (openFileDialog.ShowDialog() == true)
                dataPath = openFileDialog.FileName;
        }
    }
}

