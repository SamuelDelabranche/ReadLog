using ReadLog.MVVM.Models;
using ReadLog.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ReadLog.MVVM.Views
{
    /// <summary>
    /// Interaction logic for MangaView.xaml
    /// </summary>
    public partial class MangaView : UserControl
    {
        public MangaView()
        {
            InitializeComponent();
        }

        private void DataListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListView listView && listView.SelectedItem is Manga selectedManga)
            {
                var viewModel = DataContext as MangaViewModel;
                if (viewModel?.EditionMangaCommand != null)
                {
                    viewModel.EditionMangaCommand.Execute(selectedManga);
                }
            }
        }
    }
}
