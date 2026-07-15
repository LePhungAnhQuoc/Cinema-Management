using System.Windows;
using System.Windows.Controls;

namespace AnhQuoc_WPF_C1_B1.Helpers
{
    public static class ScrollViewerBehavior
    {
        public static readonly DependencyProperty HorizontalOffsetProperty =
            DependencyProperty.RegisterAttached(
                "HorizontalOffset",
                typeof(double),
                typeof(ScrollViewerBehavior),
                new PropertyMetadata(0.0, OnHorizontalOffsetChanged));

        public static double GetHorizontalOffset(DependencyObject obj) => (double)obj.GetValue(HorizontalOffsetProperty);
        public static void SetHorizontalOffset(DependencyObject obj, double value) => obj.SetValue(HorizontalOffsetProperty, value);

        private static void OnHorizontalOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ScrollViewer scrollViewer)
            {
                scrollViewer.ScrollToHorizontalOffset((double)e.NewValue);
            }
        }
    }
}