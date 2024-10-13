using System;
using System.Collections.Generic;
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
    /// Interaction logic for MangaEditionView.xaml
    /// </summary>
    public partial class MangaEditionView : UserControl
    {
        public MangaEditionView()
        {
            InitializeComponent();
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !isText(e.Text);
        }

        private bool isText(string text)
        {
            return (int.TryParse(text, out _));
        }
    }
}
