using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using WPFCustomMessageBox;

namespace AnhQuoc_WPF_C1_B1
{
    /// <summary>
    /// Interaction logic for frmOrder.xaml
    /// </summary>
    public partial class frmOrder : Window, INotifyPropertyChanged
    {
        public Func<Order> getOrder { get; set; }
        public bool frmReply { get; set; }

        private Order _Order;
        public Order Order
        {
            get { return _Order; }
            set
            {
                _Order = value;
                OnPropertyChanged("Order");
            }
        }
        public frmOrder()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Order = getOrder();
            frmReply = false;
            lblOrderDate.Content = Order.Date.ToString(Constants.formatDate);
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msbResult = CustomMessageBox.ShowOKCancel(
            "The printer is not connected!",
            "Unconnected device",
            "Save this booking",
            "Cancel");

            if (msbResult == MessageBoxResult.OK)
                this.frmReply = true;
            else
                this.frmReply = false;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.frmReply = false;
            this.Close();
        }
    }
}
