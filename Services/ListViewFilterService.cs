using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadLog.Services
{
    public interface IListViewFilterService
    {
        event Action<string> FilterTextChanged;
        void SetFilterText(string text);
    }
    class ListViewFilterService : IListViewFilterService
    {
        public event Action<string> FilterTextChanged;

        public void SetFilterText(string text)
        {
            FilterTextChanged?.Invoke(text);
        }
    }
}
