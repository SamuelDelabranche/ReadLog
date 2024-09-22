using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace ReadLog.Converters
{
    public class BoolToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value is bool currentBoolean)
                {
                    string imagePath = "pack://application:,,,/Assets/Images/";
                    if (currentBoolean) { imagePath += "like.png"; }
                    else { imagePath += "unlike.png"; }

                    return new BitmapImage(new Uri(imagePath));

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);                
            }return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
