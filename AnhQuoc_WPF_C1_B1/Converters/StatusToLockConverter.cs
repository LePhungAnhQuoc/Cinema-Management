using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AnhQuoc_WPF_C1_B1
{
    public class StatusToLockConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool getValue = System.Convert.ToBoolean(value);

            if (getValue)
                return "M12 2a4 4 0 0 1 4 4v2h1.75A2.25 2.25 0 0 1 20 10.25v9.5A2.25 2.25 0 0 1 17.75 22H6.25A2.25 2.25 0 0 1 4 19.75v-9.5A2.25 2.25 0 0 1 6.25 8H8V6a4 4 0 0 1 4-4Zm5.75 7.5H6.25a.75.75 0 0 0-.75.75v9.5c0 .414.336.75.75.75h11.5a.75.75 0 0 0 .75-.75v-9.5a.75.75 0 0 0-.75-.75Zm-5.75 4a1.5 1.5 0 1 1 0 3 1.5 1.5 0 0 1 0-3Zm0-10A2.5 2.5 0 0 0 9.5 6v2h5V6A2.5 2.5 0 0 0 12 3.5Z";
            return "M12 2.004c1.875 0 3.334 1.206 3.928 3.003a.75.75 0 1 1-1.425.47C14.102 4.262 13.185 3.504 12 3.504c-1.407 0-2.42.958-2.496 2.551l-.005.195v1.749h8.251a2.25 2.25 0 0 1 2.245 2.097l.005.154v9.496a2.25 2.25 0 0 1-2.096 2.245l-.154.005H6.25A2.25 2.25 0 0 1 4.005 19.9L4 19.746V10.25a2.25 2.25 0 0 1 2.096-2.245L6.25 8l1.749-.001v-1.75C8 3.712 9.71 2.005 12 2.005ZM17.75 9.5H6.25a.75.75 0 0 0-.743.648l-.007.102v9.496c0 .38.282.693.648.743l.102.007h11.5a.75.75 0 0 0 .743-.648l.007-.102V10.25a.75.75 0 0 0-.648-.744L17.75 9.5Zm-5.75 4a1.499 1.499 0 1 1 0 2.996 1.499 1.499 0 0 1 0-2.997Z";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
