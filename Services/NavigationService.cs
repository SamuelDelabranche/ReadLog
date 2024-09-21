using ReadLog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadLog.Services
{
    public interface INavigationService
    {
        public ViewModelBase CurrentView { get; }
        void NavigateTo<TViewModel>() where TViewModel : ViewModelBase;

    }

    public class NavigationService : Observable, INavigationService
    {
        private readonly IViewModelFactory _viewModelFactory;
        public NavigationService(IViewModelFactory viewModelFactory) => _viewModelFactory = viewModelFactory;

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
            CurrentView = _viewModelFactory.CreateViewModel(typeof(TViewModel));
        }
    }
}
