using ReadLog.Core;
using ReadLog.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadLog.MVVM.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public HomeViewModel(INavigationService navigationService, IDataService<ItemBase> dataService) : base(navigationService, dataService)
        {
        }
    }
}
