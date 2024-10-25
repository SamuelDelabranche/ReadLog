using ReadLog.Core;
using ReadLog.MVVM.Models;
using ReadLog.MVVM.Views;
using ReadLog.Services;
using ReadLog.Stores;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ReadLog.MVVM.ViewModels
{
    public class MangaEditionViewModel : ViewModelBase, IParameterNavigationService
    {

        public MangaEditionViewModel(INavigationService navigationService, DataStore<Manga> dataStore) : base(navigationService, dataStore)
        {
            ValidationCommand = new RelayCommand(execute => updateInformations());
        }

        private async void updateInformations()
        {
            await _dataStore.UpdateMangaAsync(Item);
            _navigationService.NavigateTo<MangaViewModel>();
        }

        public RelayCommand ValidationCommand { get; set; }

        private ImageSource _imageManga;
        public ImageSource ImageManga
        {
            get
            {
                return _imageManga;
            }
            set
            {
                _imageManga = value;
                OnPropertyChanged(nameof(ImageManga));
            }
        }

        private Manga _item;
        public Manga Item
        {
            get
            {
                return _item;
            }
            set
            {
                _item = value;
                OnPropertyChanged(nameof(Item));
                _ = LoadImageAsync();
            }
        }

        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        private bool _isFavorite;
        public bool Favorite
        {
            get
            {
                return _isFavorite;
            }
            set
            {
                _isFavorite = value;
                Item.IsFavorite = _isFavorite;
                OnPropertyChanged(nameof(Favorite));
            }
        }

        private async Task LoadImageAsync()
        {
            var imageSource = await _dataStore.LoadImageAsync(Item);
            if (imageSource != null)
            {
                ImageManga = imageSource;
            }
        }

        public void ReceiverParameter(object parameter)
        {
            if (parameter is Manga manga) Item = manga;
            if (Item != null)
            {
                Description = Item.Description;
                Favorite = Item.IsFavorite;
            }
        }
        public void Close() => _navigationService.NavigateTo<MangaViewModel>();
    }
}
