using ReadLog.Core;
using ReadLog.Services;
using ReadLog.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReadLog.MVVM.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ReadLog.MVVM.Views;
using System.Windows;
using System.Diagnostics;

namespace ReadLog.MVVM.ViewModels
{
    public class MangaViewModel : ViewModelBase
    {

        public MessageViewModel ErrorMessage { get; }
        public RelayCommand AddMangaCommand { get; }
        public RelayCommand EditionMangaCommand { get; set; }

        private string _filterText;
        private ObservableCollection<Manga> Items;
        public ObservableCollection<Manga> FilteredItems { get; set; }

        private readonly IListViewFilterService _listViewFilterService;
        private readonly IServiceProvider _serviceProvider;

        private readonly Window _addMangaWindow;
        public MangaViewModel(INavigationService navigationService, DataStore<Manga> dataStore, IListViewFilterService listViewFilterService, IServiceProvider serviceProvider) : base(navigationService, dataStore)
        {
            if (_dataStore.Status.HasMessage)
            {
                ErrorMessage = _dataStore.Status;
                Items = null;
                FilteredItems = null;
            }
            else
            {
                ErrorMessage = new MessageViewModel();
                Items = _dataStore.Items;
                FilteredItems = new ObservableCollection<Manga>(Items);
            }


            AddMangaCommand = new RelayCommand(execute => NavigateAddView(), canExecute => !ErrorMessage.HasMessage);
            EditionMangaCommand = new RelayCommand(execute => OnItemDoubleClick((Manga)execute));

            _serviceProvider = serviceProvider;

            _addMangaWindow = serviceProvider.GetRequiredService<AddMangaWindow>();
            _addMangaWindow.Owner = serviceProvider.GetRequiredService<MainWindow>();

            _listViewFilterService = listViewFilterService;
            _listViewFilterService.FilterTextChanged += OnFilterTextChanged;
            _dataStore.itemAddedWithMangaReturned += OnItemAdded;
        }
        private void OnItemDoubleClick(Manga selectedItem)
        {
            if (selectedItem != null)
            {
                _navigationService.NavigateTo<MangaEditionViewModel>(selectedItem);
            }
        }

        private void OnItemAdded(Manga newManga)
        {
            FilteredItems.Add(newManga);
        }

        private void OnFilterTextChanged(string obj)
        {
            _filterText = obj.ToLower();
            FilteredItems.Clear();

            if (string.IsNullOrEmpty(_filterText))
            {
                foreach (var item in Items) { FilteredItems.Add(item); }
            }

            else
            {
                var itemFiltered = Items.Where(manga => manga.Name.ToLower().Contains(_filterText)).ToList();
                foreach (var item in itemFiltered) { FilteredItems.Add(item); }
            }

        }

        private void NavigateAddView()
        {
            _addMangaWindow.ShowDialog();
        }
    }
}
