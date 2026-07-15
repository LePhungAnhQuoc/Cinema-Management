using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace AnhQuoc_WPF_C1_B1.Converters
{
    public class TreeViewMarginConverter : IValueConverter
    {
        public double Length { get; set; } = 19; // The width of each indentation level

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var item = value as TreeViewItem;
            if (item == null) return new Thickness(0);

            return new Thickness(Length * GetDepth(item), 0, 0, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        // Helper method to walk up the visual tree and calculate item depth
        private static int GetDepth(TreeViewItem item)
        {
            int depth = 0;
            DependencyObject parent = VisualTreeHelper.GetParent(item);

            while (parent != null && !(parent is TreeView))
            {
                if (parent is TreeViewItem)
                {
                    depth++;
                }
                parent = VisualTreeHelper.GetParent(parent);
            }
            return depth;
        }
    }
}