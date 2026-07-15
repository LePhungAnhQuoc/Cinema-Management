using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AnhQuoc_WPF_C1_B1.UserControls.Cashier
{
    /// <summary>
    /// Interaction logic for ucBankTransfer.xaml
    /// </summary>
    public partial class ucBankTransfer : UserControl
    {
        #region GetData


        public double TotalPrice
        {
            get { return (double)GetValue(TotalPriceProperty); }
            set { SetValue(TotalPriceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TotalPrice.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TotalPriceProperty =
            DependencyProperty.Register(nameof(TotalPrice), typeof(double), typeof(ucBankTransfer), new PropertyMetadata(0.0));


        #endregion
        public ucBankTransfer()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private async void BtnCopy_Click(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button btn))
                return;

            // Determine text to copy: prefer Tag (bound to the value TextBlock), fallback to Content as string
            var textToCopy = btn.Tag as string;
            if (string.IsNullOrWhiteSpace(textToCopy))
            {
                return;
            }

            if (string.IsNullOrEmpty(textToCopy))
                return;

            try
            {
                Clipboard.SetText(textToCopy);

                // Provide immediate feedback by changing the button text briefly
                var originalContent = btn.Content;
                btn.Content = "Đã sao chép";
                btn.IsEnabled = false;

                await Task.Delay(1500);

                btn.Content = originalContent;
                btn.IsEnabled = true;
            }
            catch (Exception)
            {
                // Clipboard access can fail in some contexts; fail silently or you can show a message
            }
        }
    }
}
