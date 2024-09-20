using ReadLog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadLog.Services
{
    public class NavigationService : ViewModelBase, INavigationService
    {
		private ViewModelBase _currentView;
		public ViewModelBase CurrentView
        {
			get
			{
				return _currentView;
			}
			set
			{
				_currentView = value;
				OnPropertyChanged(nameof(CurrentView));
			}
		}
		public void NavigateTo<TViewModel>() where TViewModel : ViewModelBase
        {
            throw new NotImplementedException();
        }
    }
}
