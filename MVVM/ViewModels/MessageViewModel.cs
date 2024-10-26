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

    public class MessageViewModel : ViewModelBase
    {
        public MessageViewModel(INavigationService navigationService, DataStore<Manga> dataStore) : base(navigationService, dataStore)
        {
            Content = string.Empty;
        }

        public MessageViewModel()
        {
            Content = string.Empty;
        }

        private string _content;
        public string Content
        {
            get
            {
                return _content;
            }
            set
            {
                _content = value;
                OnPropertyChanged(nameof(Content));
                OnPropertyChanged(nameof(HasMessage));
            }
        }

        public bool HasMessage => !string.IsNullOrEmpty(Content);
        public void DisplayMessage(CustomExceptionBase customException) { Content = customException.Message; }
        public void DisplayMessage(string message)
        {
            CustomExceptionBase customException = new CustomExceptionBase(message);
            Content = customException.Message;
        }
        public void ClearMessage() { Content = string.Empty; }
    }
}
