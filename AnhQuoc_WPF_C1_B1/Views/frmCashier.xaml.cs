using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AnhQuoc_WPF_C1_B1
{
    /// <summary>
    /// Interaction logic for frmCashier.xaml
    /// </summary>
    public partial class frmCashier : Window
    {
        public Func<RepositoryBase<Order>> getOrderRepo { get; set; }
        public Func<RepositoryBase<Movie>> getMovieRepo { get; set; }
        public Func<RepositoryBase<MovieSchedule>> getMovieScheduleRepo { get; set; }
        public Func<RepositoryBase<Cinema>> getCinemaRepo { get; set; }
        public Func<RepositoryBase<Customer>> getCustomerRepo { get; set; }

        public Func<frmLogin> getFrmLogin { get; set; }
        public frmCashier()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void tvViewTicketPrice_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ucViewTicketPrice ucViewTicketPrice = new ucViewTicketPrice();
            ucViewTicketPrice.getCinemaRepo = getCinemaRepo;
            gdView.Children.Clear();
            gdView.Children.Add(ucViewTicketPrice);
        }

        private void tvBooking_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ucBooking ucBooking = new ucBooking();
            ucBooking.getOrderRepo = getOrderRepo;
            ucBooking.getMovieScheduleRepo = getMovieScheduleRepo;
            ucBooking.getCinemaRepo = getCinemaRepo;
            ucBooking.getCustomerRepo = getCustomerRepo;
            
            gdView.Children.Clear();
            gdView.Children.Add(ucBooking);
        }

        private void tvLogOut_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Hide();
            getFrmLogin().ClearLogin();
            getFrmLogin().Show();
            getFrmLogin().txtUsername.Focus();
        }
    }
}
