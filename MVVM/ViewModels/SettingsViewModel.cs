using Microsoft.Win32;
using ReadLog.Core;
using ReadLog.MVVM.Models;
using ReadLog.Services;
using ReadLog.Stores;
using System;
using System.Collections.Generic;
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
        public SettingsViewModel(INavigationService navigationService, DataStore<Manga> dataStore) : base(navigationService, dataStore)
        {

            try
            {
                dataPath = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName, "Stores/Data.json");
            }
            catch
            {

            }

            SelectedData = new RelayCommand(execute => OpenFileSelector());
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

