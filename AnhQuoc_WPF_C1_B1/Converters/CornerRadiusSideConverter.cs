using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace AnhQuoc_WPF_C1_B1.Converters
{
    public class CornerRadiusSideConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is CornerRadius parentRadius)
            {
                string side = parameter as string;
                double r = parentRadius.TopLeft; // Grab the primary radius value

                if (side == "Left")
                    return new CornerRadius(r, 0, 0, r); // TopLeft, TopRight, BottomRight, BottomLeft
                if (side == "Right")
                    return new CornerRadius(0, r, r, 0);
                if (side == "Top")
                    return new CornerRadius(r, r, 0, 0);
                if (side == "Bottom")
                    return new CornerRadius(0, 0, r, r);
            }
            return new CornerRadius(0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
