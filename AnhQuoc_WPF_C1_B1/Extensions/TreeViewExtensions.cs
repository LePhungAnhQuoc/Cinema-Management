using System.Windows;
using System.Windows.Media;

namespace AnhQuoc_WPF_C1_B1
{
    public static class TreeViewExtensions
    {
        public static readonly DependencyProperty IconSourceProperty =
            DependencyProperty.RegisterAttached(
                "IconSource",
                typeof(ImageSource),
                typeof(TreeViewExtensions),
                new PropertyMetadata(null));

        public static void SetIconSource(DependencyObject element, ImageSource value)
        {
            element.SetValue(IconSourceProperty, value);
        }

        public static ImageSource GetIconSource(DependencyObject element)
        {
            return (ImageSource)element.GetValue(IconSourceProperty);
        }
    }
}