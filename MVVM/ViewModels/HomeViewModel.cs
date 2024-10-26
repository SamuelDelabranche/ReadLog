using ReadLog.Core;
using ReadLog.MVVM.Models;
using ReadLog.Services;
using ReadLog.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ReadLog.MVVM.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private ImageSource _firstImage;
        public ImageSource firstImage
        {
            get
            {
                return _firstImage;
            }
            set
            {
                _firstImage = value;
                OnPropertyChanged(nameof(firstImage));
            }
        }

        private ImageSource _secondImage;
        public ImageSource secondImage
        {
            get
            {
                return _secondImage;
            }
            set
            {
                _secondImage = value;
                OnPropertyChanged(nameof(secondImage));
            }
        }

        private ImageSource _thirdImage;
        public ImageSource thirdImage
        {
            get
            {
                return _thirdImage;
            }
            set
            {
                _thirdImage = value;
                OnPropertyChanged(nameof(thirdImage));
            }
        }

        private string _firstImageTitle;
        public string firstImageTitle
        {
            get
            {
                return _firstImageTitle;
            }
            set
            {
                _firstImageTitle = value;
                OnPropertyChanged(nameof(firstImageTitle));
            }
        }

        private string _secondImageTitle;
        public string secondImageTitle
        {
            get
            {
                return _secondImageTitle;
            }
            set
            {
                _secondImageTitle = value;
                OnPropertyChanged(nameof(secondImageTitle));
            }
        }

        private string _thirdImageTitle;
        public string thirdImageTitle
        {
            get
            {
                return _thirdImageTitle;
            }
            set
            {
                _thirdImageTitle = value;
                OnPropertyChanged(nameof(thirdImageTitle));
            }
        }

        public ObservableCollection<Manga> Items { get; set; }
        public MessageViewModel DataError { get; set; }
        public HomeViewModel(INavigationService navigationService, DataStore<Manga> dataStore) : base(navigationService, dataStore)
        {
            DataError = new MessageViewModel();
            if (_dataStore.Status.HasMessage)
            {
                DataError = _dataStore.Status;
                Items = null;
            }
            else
            {
                Items = _dataStore.Items;
            }

            UpdatePlaces();
            _dataStore.itemAdded += UpdatePlaces;

        }

        private async void UpdatePlaces()
        {
            Items = _dataStore.Items;

            if (Items.Count >= 1)
            {
                firstImage = await _dataStore.LoadImageAsync(Items.Last());
                firstImageTitle = await _dataStore.GetMangaTitleAsync(Items.Last());
            }
            if (Items.Count >= 2)
            {
                secondImage = await _dataStore.LoadImageAsync(Items[Items.Count - 2]);
                secondImageTitle = await _dataStore.GetMangaTitleAsync(Items[Items.Count - 2]);
            }
            if (Items.Count >= 3)
            {
                thirdImage = await _dataStore.LoadImageAsync(Items[Items.Count - 3]);
                thirdImageTitle = await _dataStore.GetMangaTitleAsync(Items[Items.Count - 3]);
            }
        }
    }
}
