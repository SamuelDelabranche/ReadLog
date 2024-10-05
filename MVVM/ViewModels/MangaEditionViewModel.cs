using ReadLog.Core;
using ReadLog.MVVM.Models;
using ReadLog.Services;
using ReadLog.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadLog.MVVM.ViewModels
{
    public class MangaEditionViewModel : ViewModelBase, IParameterNavigationService
    {
        public MangaEditionViewModel(INavigationService navigationService, DataStore<Manga> dataStore) : base(navigationService, dataStore)
        {

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
            }
        }
        public void ReceiverParameter(object parameter)
        {
            if (parameter is Manga manga) Item = manga;
        }
    }
}
