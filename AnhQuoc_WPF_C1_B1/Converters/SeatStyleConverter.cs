using AnhQuoc_WPF_C1_B1.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace AnhQuoc_WPF_C1_B1.Converters
{
    public class SeatStyleConverter : IValueConverter
    {
        public Style DefaultStyle { get; set; }
        public Style SelectedStyle { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SeatType seatType && seatType == SeatType.Selected)
            {
                return SelectedStyle;
            }
            return DefaultStyle;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
