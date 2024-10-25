﻿using ReadLog.Core;
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
            ErrorMessage = new MessageViewModel(navigationService, dataStore);

            InitData(dataStore);

            AddMangaCommand = new RelayCommand(execute => NavigateAddView(), canExecute => !ErrorMessage.HasMessage);
            EditionMangaCommand = new RelayCommand(execute => OnItemDoubleClick((Manga)execute));

            _serviceProvider = serviceProvider;

            var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
            _addMangaWindow = serviceProvider.GetRequiredService<AddMangaWindow>();

            _addMangaWindow.Owner = mainWindow;

            _listViewFilterService = listViewFilterService;
            _listViewFilterService.FilterTextChanged += OnFilterTextChanged;
            _dataStore.itemAdded += OnItemAdded;
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

        private async void InitData(DataStore<Manga> dataStore)
        {
            try
            {
                await dataStore.LoadDataAsync();
                Items = dataStore.Items;
                FilteredItems = new ObservableCollection<Manga>(Items);

            }
            catch (CustomExceptionBase ex)
            {
                ErrorMessage.DisplayMessage(ex.Message);
            }

        }
    }
}
