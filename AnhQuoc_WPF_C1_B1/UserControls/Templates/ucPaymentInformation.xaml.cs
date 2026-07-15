using AnhQuoc_WPF_C1_B1.Enums;
using AnhQuoc_WPF_C1_B1.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace AnhQuoc_WPF_C1_B1.UserControls.Templates
{
    /// <summary>
    /// Interaction logic for ucPaymentInformation.xaml
    /// </summary>
    public partial class ucPaymentInformation : UserControlBase
    {

        #region GetData


        public frmBookingTicket frmBookingTicket
        {
            get { return (frmBookingTicket)GetValue(frmBookingTicketProperty); }
            set { SetValue(frmBookingTicketProperty, value); }
        }

        // Using a DependencyProperty as the backing store for frmBookingTicket.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty frmBookingTicketProperty =
            DependencyProperty.Register(nameof(frmBookingTicket), typeof(frmBookingTicket), typeof(ucPaymentInformation), new PropertyMetadata(null));



        public bool IsUserLoggedIn
        {
            get { return (bool)GetValue(IsLogInProperty); }
            set { SetValue(IsLogInProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsLogIn.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsLogInProperty =
            DependencyProperty.Register(nameof(IsUserLoggedIn), typeof(bool), typeof(ucPaymentInformation), new PropertyMetadata(false));



        public DateSchedule DateSchedule
        {
            get { return (DateSchedule)GetValue(DateScheduleProperty); }
            set { SetValue(DateScheduleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DateSchedule.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DateScheduleProperty =
            DependencyProperty.Register(nameof(DateSchedule), typeof(DateSchedule), typeof(ucPaymentInformation), new PropertyMetadata(null));


        public TimeSchedule TimeSchedule
        {
            get { return (TimeSchedule)GetValue(TimeScheduleProperty); }
            set { SetValue(TimeScheduleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TimeSchedule.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TimeScheduleProperty =
            DependencyProperty.Register(nameof(TimeSchedule), typeof(TimeSchedule), typeof(ucPaymentInformation), new PropertyMetadata(null));




        public ObservableCollection<Seat> SelectedSeats
        {
            get { return (ObservableCollection<Seat>)GetValue(SelectedSeatsProperty); }
            set { SetValue(SelectedSeatsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedSeats.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedSeatsProperty =
            DependencyProperty.Register(nameof(SelectedSeats), typeof(ObservableCollection<Seat>), typeof(ucPaymentInformation), new PropertyMetadata(null));



        public Movie Movie
        {
            get { return (Movie)GetValue(MovieProperty); }
            set { SetValue(MovieProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Movie.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MovieProperty =
            DependencyProperty.Register(nameof(Movie), typeof(Movie), typeof(ucPaymentInformation), new PropertyMetadata(null));
        #endregion

        #region Properties
        private Account _Account;
        public Account Account
        {
            get { return _Account; }
            set 
            { 
                _Account = value;
                OnPropertyChanged();
            }
        }

        private string quantityBySeatTypesSummary;
        public string QuantityBySeatTypesSummary
        {
            get { return quantityBySeatTypesSummary; }
            set 
            { 
                quantityBySeatTypesSummary = value;
                OnPropertyChanged();
            }
        }

        private double _TotalSeatPrice;
        public double TotalSeatPrice
        {
            get { return _TotalSeatPrice; }
            set 
            { 
                _TotalSeatPrice = value;
                OnPropertyChanged();
            }
        }

        private double _TotalPrice;

        public double TotalPrice
        {
            get { return _TotalPrice; }
            set 
            { 
                _TotalPrice = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public ucPaymentInformation()
        {
            InitializeComponent();
            this.Loaded += UcPaymentInformation_Loaded;
            this.DataContext = this;
        }

        private void UcPaymentInformation_Loaded(object sender, RoutedEventArgs e)
        {
            IsUserLoggedIn = SessionManager.Instance.IsLoggedIn;
            Account = SessionManager.Instance.CurrentUser;

            var quantityBySeatTypes = GetQuantityBySeatTypes();

            QuantityBySeatTypesSummary = string.Join(", ", quantityBySeatTypes.Select(x => $"Vé {x.SeatType} x {x.Quantity}"));
            
            foreach (Seat seat in SelectedSeats)
            {
                TotalSeatPrice += seat.Price;
            }
            TotalPrice = TotalSeatPrice;
        }

        private List<QuantityBySeatType> GetQuantityBySeatTypes()
        {
            List<QuantityBySeatType> listQuantityBySeatType = new List<QuantityBySeatType>();
            foreach (Seat seat in SelectedSeats)
            {
                QuantityBySeatType getItem = listQuantityBySeatType.FirstOrDefault(item => item.SeatType == seat.Type);

                if (getItem == null)
                {
                    getItem = new QuantityBySeatType();
                    getItem.SeatType = seat.Type;
                    getItem.Quantity = 1;

                    listQuantityBySeatType.Add(getItem);
                }
                else
                {
                    getItem.Quantity++;
                }
            }
            return listQuantityBySeatType;
        }

        private void PaymentMethods_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            // Check if the event is already handled
            if (!e.Handled)
            {
                // Mark it handled here to stop the ScrollViewer from eating it
                e.Handled = true;

                // Create a new MouseWheelEventArgs routed event to bubble up
                MouseWheelEventArgs eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta)
                {
                    RoutedEvent = UIElement.MouseWheelEvent,
                    Source = sender
                };

                // Raise the event on the parent control so the outer window/scrollviewer handles it
                var parent = ((Control)sender).Parent as UIElement;
                parent?.RaiseEvent(eventArg);
            }
        }

        private void PaymentMethods_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TxtEmail_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Don't let the keypress bubble up further
                e.Handled = true;

                BtnSaveUserInformation_Click(btnSaveUserInfo, new RoutedEventArgs());
            }
        }

        private void BtnSaveUserInformation_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnGoBack_Click(object sender, RoutedEventArgs e)
        {
            frmBookingTicket.GoBack();
        }

        private void BtnPayment_Click(object sender, RoutedEventArgs e)
        {
            frmBookingTicket.GoToBankTransfer();
        }
    }

    public class QuantityBySeatType
    {
        public SeatType SeatType { get; set; }
        public int Quantity { get; set; }
    }
}
