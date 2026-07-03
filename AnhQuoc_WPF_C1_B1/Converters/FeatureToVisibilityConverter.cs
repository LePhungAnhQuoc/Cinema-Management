using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace AnhQuoc_WPF_C1_B1.Converters
{
    public class FeatureToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string getValue = (string)value;

            if (getValue != null)
            {
                if (getValue == "add")
                {
                    if ((string)parameter == "Collapse")
                    {
                        return Visibility.Collapsed;
                    }
                    return Visibility.Visible;
                }
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
