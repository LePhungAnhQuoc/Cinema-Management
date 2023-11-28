using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace AnhQuoc_WPF_C1_B1
{
    /// <summary>
    /// Interaction logic for ucStatistical.xaml
    /// </summary>
    public partial class ucStatistical : UserControl
    {
        public Func<RepositoryBase<Order>> getOrderRepo { get; set; }

        public ucStatistical()
        {
            InitializeComponent();
        }
        
        private void date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            OrderViewModel orderVM = new OrderViewModel();
            orderVM.OrderRepo = getOrderRepo();

            if (date.SelectedDate == null)
                return;
            DateTime dateValue = Convert.ToDateTime(date.SelectedDate);
            RepositoryBase<Order> lstOrder = orderVM.FillByDate(dateValue);
            dgOrders.ItemsSource = new ObservableCollection<Order>(lstOrder.Items);

            OrderViewModel orderVM2 = new OrderViewModel();
            orderVM2.OrderRepo = new RepositoryBase<Order>();
            orderVM2.OrderRepo.Items = lstOrder.Items;
            txtRevenue.Text = orderVM2.TotalRevenue().ToString(Constants.formatCurrency);
        }

        private void dgOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Order order = dgOrders.SelectedItem as Order;

            if (order == null) return;
            if (order.Details == null) return;
            dgOrderDetails.ItemsSource = new ObservableCollection<OrderDetail>(order.Details);
        }
    }
}
