using System.Windows;
using System.Windows.Controls;

namespace AnhQuoc_WPF_C1_B1.Selector
{
    public class SeatTemplateSelector : DataTemplateSelector
    {
        public DataTemplate EmptyTemplate { get; set; }
        public DataTemplate SeatTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            // If the data item is null, return the empty space template
            if (item == null)
            {
                return EmptyTemplate;
            }

            // Otherwise, it's a valid seat object, so return the button template
            return SeatTemplate;
        }
    }
}