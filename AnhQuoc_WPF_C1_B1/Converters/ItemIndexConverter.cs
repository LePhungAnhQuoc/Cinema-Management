using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace AnhQuoc_WPF_C1_B1.Converters
{
    public class ItemIndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Get the container item (the individual movie layout item)
            var item = value as DependencyObject;
            if (item == null) return "1";

            // Traverse up the tree to find the parent ItemsControl container
            var itemsControl = ItemsControl.ItemsControlFromItemContainer(item);
            if (itemsControl != null)
            {
                // Find the index position number and add 1 so it displays 1, 2, 3...
                int index = itemsControl.ItemContainerGenerator.IndexFromContainer(item);
                return (index + 1).ToString();
            }

            return "1";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}