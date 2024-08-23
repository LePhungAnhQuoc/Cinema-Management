using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace AnhQuoc_WPF_C1_B1
{
    /// <summary>
    /// Interaction logic for ucScheduleBooking.xaml
    /// </summary>
    public partial class ucScheduleBooking : UserControl, INotifyPropertyChanged
    {
        public Func<MovieSchedule> getMovieSchedule { get; set; }
        public Func<ucBooking> getUcBooking { get; set; }

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        // Phương thức hỗ trợ emplement
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        private MovieSchedule _movieSchedule;
        public MovieSchedule movieSchedule
        {
            get { return _movieSchedule; }
            set
            {
                _movieSchedule = value;
                OnPropertyChanged("movieSchedule");
            }
        }

        public ucScheduleBooking()
        {
            InitializeComponent();
            this.DataContext = this; // Gởi dữ liệu
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            movieSchedule = getMovieSchedule();
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            Order newOrder = getUcBooking().newOrder;
            // Checking Is Valid Infomation input

            TimeSchedule getTimeSchedule = lbTime.SelectedItem as TimeSchedule;
            if (cbCinemaTypes.SelectedItem == null)
            {
                MessageBox.Show("Please select cinema type");
                return;
            }
            if (cbCinemas.SelectedItem == null)
            {
                MessageBox.Show("Please select cinema");
                return;
            }

            if (lbDate.SelectedItem == null)
            {
                MessageBox.Show("Please select date");
                return;
            }
            if (getTimeSchedule == null)
            {
                MessageBox.Show("Please select time");
                return;
            }
            newOrder.MovieOrder.CinemaType = ((CinemaTypeSchedule)cbCinemaTypes.SelectedItem).CinemaType;
            newOrder.MovieOrder.Cinema = ((CinemaSchedule)cbCinemas.SelectedItem).Cinema;
            newOrder.MovieOrder.Date = ((DateSchedule)lbDate.SelectedItem).Date;
            newOrder.MovieOrder.Time = ((TimeSchedule)lbTime.SelectedItem).Time;
        
            newOrder.Customer = getUcBooking().LoadFrmCustomer();
            if (newOrder.Customer == null)
            {
                getUcBooking().Content = null;
                return;
            }
            getUcBooking().getSeatsRepo = () => getTimeSchedule.Seats;
            getUcBooking().LoadUcSeatBooking();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to cancel the booking?", null, MessageBoxButton.OKCancel, MessageBoxImage.Information, MessageBoxResult.Cancel);
            if (result == MessageBoxResult.OK)
            {
                getUcBooking().Content = null;
            }
        }

        private void cbCinemaTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lblSelectCinemaType.Visibility = Visibility.Hidden;
        }

        private void cbCinemas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lblSelectCinema.Visibility = Visibility.Hidden;
        }
    }
}
