using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AnhQuoc_WPF_C1_B1.Converters
{
    [ValueConversion(typeof(string), typeof(DateTime?))]
    public class TimeFormatToDateTimeFormatConverter : IValueConverter
    {
        // Convert: Source Time String (e.g., "13:37:30") -> Target DateTime? for the TimePicker
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            string timeString = value.ToString();

            // Try parsing the string format "HH:mm:ss" or "HH:mm"
            if (TimeSpan.TryParse(timeString, out TimeSpan timeSpan))
            {
                // Returns today's date combined with the target time
                return DateTime.Today.Add(timeSpan);
            }

            return null;
        }

        // ConvertBack: TimePicker DateTime? -> Target Time String (e.g., "13:37:30") back to your ViewModel
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dateTime)
            {
                // Return it in the standard 24-hour string format matching your data logs
                return dateTime.ToString("HH:mm:ss", CultureInfo.InvariantCulture);
            }

            return null;
        }
    }
}
