using AnhQuoc_WPF_C1_B1.UserControls;
using AnhQuoc_WPF_C1_B1.UserControls.Cashier;
using AnhQuoc_WPF_C1_B1.UserControls.Templates;
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
using System.Windows.Shapes;

namespace AnhQuoc_WPF_C1_B1.Views
{
    /// <summary>
    /// Interaction logic for frmBookingTicket.xaml
    /// </summary>
    public partial class frmBookingTicket : Window, INotifyPropertyChanged
    {
        #region GetDatas
        public Cinema Cinema
        {
            get { return (Cinema)GetValue(CinemaProperty); }
            set { SetValue(CinemaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Cinema.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CinemaProperty =
            DependencyProperty.Register(nameof(Cinema), typeof(Cinema), typeof(frmBookingTicket), new PropertyMetadata(null));


        public Movie Movie
        {
            get { return (Movie)GetValue(MovieProperty); }
            set { SetValue(MovieProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Movie.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MovieProperty =
            DependencyProperty.Register(nameof(Movie), typeof(Movie), typeof(frmBookingTicket), new PropertyMetadata(null));


        public DateSchedule DateSchedule
        {
            get { return (DateSchedule)GetValue(DateScheduleProperty); }
            set { SetValue(DateScheduleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DateSchedule.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DateScheduleProperty =
            DependencyProperty.Register(nameof(DateSchedule), typeof(DateSchedule), typeof(frmBookingTicket), new PropertyMetadata(null));


        public TimeSchedule TimeSchedule
        {
            get { return (TimeSchedule)GetValue(TimeScheduleProperty); }
            set { SetValue(TimeScheduleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TimeSchedule.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TimeScheduleProperty =
            DependencyProperty.Register(nameof(TimeSchedule), typeof(TimeSchedule), typeof(frmBookingTicket), new PropertyMetadata(null));




        public ObservableCollection<Seat> SelectedSeats
        {
            get { return (ObservableCollection<Seat>)GetValue(SelectedSeatsProperty); }
            set { SetValue(SelectedSeatsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedSeats.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedSeatsProperty =
            DependencyProperty.Register(nameof(SelectedSeats), typeof(ObservableCollection<Seat>), typeof(frmBookingTicket), new PropertyMetadata(null));


        #endregion

        // 1. Cache instances of your UserControls so they remember their state/inputs
        private ucDisplaySeats _seatSelectionView;
        private ucPaymentInformation _paymentInfoView;
        private ucBankTransfer _ucBankTransfer;

        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public frmBookingTicket()
        {
            InitializeComponent();
            Loaded += FrmBookingTicket_Loaded;
        }

        private void FrmBookingTicket_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = this; // Bind to self for simplicity

            // 2. Initialize the views once
            _seatSelectionView = new ucDisplaySeats();
            _seatSelectionView.frmBookingTicket = this;
            _seatSelectionView.Cinema = Cinema;
            _paymentInfoView = new ucPaymentInformation();
            _paymentInfoView.frmBookingTicket = this;
            _paymentInfoView.DateSchedule = DateSchedule;
            _paymentInfoView.TimeSchedule = TimeSchedule;
            _paymentInfoView.SelectedSeats = _seatSelectionView.SelectedSeats;
            _paymentInfoView.Movie = Movie;
            _ucBankTransfer = new ucBankTransfer();
            _ucBankTransfer.TotalPrice = _paymentInfoView.TotalPrice;

            // 3. Set the starting view
            CurrentView = _seatSelectionView;
        }

        public void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            if (SessionManager.Instance.IsLoggedIn == false)
            {
                frmLogin frmLogin = new frmLogin();
                frmLogin.ShowDialog();

                frmLogin.RoleNavigation();
            }

            // 4. Swap to the payment view when "Next" is hit
            if (CurrentView == _seatSelectionView)
            {
                CurrentView = _paymentInfoView;
            }
        }

        public void GoBack()
        {
            if (CurrentView == _paymentInfoView)
            {
                CurrentView = _seatSelectionView;
            }
        }

        public void GoToBankTransfer()
        {
            if (CurrentView == _paymentInfoView)
            {
                CurrentView = _ucBankTransfer;
                _ucBankTransfer.TotalPrice = _paymentInfoView.TotalPrice;
            }
        }

        // Property tracking boilerplate
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
