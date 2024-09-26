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

namespace ReadLog.MVVM.ViewModels
{
    public class MangaViewModel : ViewModelBase
    {

        public RelayCommand AddMangaCommand { get; }

        private string _filterText;
        private ObservableCollection<Manga> Items;
        public ObservableCollection<Manga> FilteredItems { get; }

        private readonly IListViewFilterService _listViewFilterService;
        public MangaViewModel(INavigationService navigationService, DataStore<Manga> dataStore, IListViewFilterService listViewFilterService) : base(navigationService, dataStore)
        {
            Items = dataStore.Items;
            FilteredItems = new ObservableCollection<Manga>(Items);
            _listViewFilterService = listViewFilterService;

            AddMangaCommand = new RelayCommand(execute => NavigateAddView());

            _listViewFilterService.FilterTextChanged += OnFilterTextChanged;
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

        }

    }
}
