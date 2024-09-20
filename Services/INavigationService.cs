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
}
