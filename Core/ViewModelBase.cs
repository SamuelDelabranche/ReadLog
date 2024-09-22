using ReadLog.MVVM.Models;
using ReadLog.Services;
using ReadLog.Stores;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ReadLog.Core
{
    public class ViewModelBase : Observable
    { 
        protected readonly INavigationService _navigationService;
        private readonly DataStore<Manga> _datatStore;

        public ViewModelBase(INavigationService navigationService, DataStore<Manga> dataStore)
        {
            _navigationService = navigationService;
            _datatStore = dataStore;
        }
    }
}
