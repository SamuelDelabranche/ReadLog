using ReadLog.Services;
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
        private readonly IDataService<ItemBase> _dataService;

        public ViewModelBase(INavigationService navigationService, IDataService<ItemBase> dataService)
        {
            _navigationService = navigationService;
            _dataService = dataService;
        }
    }
}
