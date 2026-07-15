using AnhQuoc_WPF_C1_B1.UserControls.Templates;
using AnhQuoc_WPF_C1_B1.Views;
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

namespace AnhQuoc_WPF_C1_B1.UserControls
{
    /// <summary>
    /// Interaction logic for ucTicketSummary.xaml
    /// </summary>
    public partial class ucTicketSummary : UserControl, INotifyPropertyChanged
    {
        #region Fields
        #endregion

        #region GetData
        public frmBookingTicket frmBookingTicket
        {
            get { return (frmBookingTicket)GetValue(frmBookingTicketProperty); }
            set { SetValue(frmBookingTicketProperty, value); }
        }

        // Using a DependencyProperty as the backing store for frmBookingTicket.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty frmBookingTicketProperty =
            DependencyProperty.Register(nameof(frmBookingTicket), typeof(frmBookingTicket), typeof(ucTicketSummary), new PropertyMetadata(null));

        public ObservableCollection<Seat> SeatsSource
        {
            get { return (ObservableCollection<Seat>)GetValue(SeatsSourceProperty); }
            set { SetValue(SeatsSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SeatsSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SeatsSourceProperty =
            DependencyProperty.Register(nameof(SeatsSource), typeof(ObservableCollection<Seat>), typeof(ucTicketSummary), new PropertyMetadata(null));

        public ObservableCollection<Seat> SelectedSeats
        {
            get { return (ObservableCollection<Seat>)GetValue(SelectedSeatsProperty); }
            set { SetValue(SelectedSeatsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedSeats.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedSeatsProperty =
            DependencyProperty.Register(nameof(SelectedSeats), typeof(ObservableCollection<Seat>), typeof(ucTicketSummary), new PropertyMetadata(new ObservableCollection<Seat>()));
        #endregion

        #region Properties


        public double EstimatedPrice
        {
            get { return (double)GetValue(EstimatedPriceProperty); }
            set { SetValue(EstimatedPriceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EstimatedPrice.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EstimatedPriceProperty =
            DependencyProperty.Register(nameof(EstimatedPrice), typeof(double), typeof(ucTicketSummary), new PropertyMetadata(0.0));



        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        public ucTicketSummary()
        {
            InitializeComponent();
            SelectedSeats.CollectionChanged += SelectedSeats_CollectionChanged;
        }

        public void SelectedSeats_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                foreach (Seat item in e.OldItems)
                {
                    ResetSeat(item);
                    EstimatedPrice -= item.Price;
                }
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Reset)
            {
                foreach (Seat item in e.OldItems)
                {
                    ResetSeat(item);
                    EstimatedPrice -= item.Price;
                }
            }
        }

        public void ResetSeat(Seat removedSeat)
        {
            Seat sourceSeat = null;
            foreach (Seat seat in SeatsSource)
            {
                if (seat != null && seat.Id == removedSeat.Id)
                {
                    sourceSeat = seat; 
                    break;
                }
            }
            if (sourceSeat == null)
            {
                Utilities.HandleError();
            }
            sourceSeat.Type = removedSeat.Type;
        }

        public void ResetSeat(ObservableCollection<Seat> removedSeats)
        {
            foreach (var item in removedSeats)
            {
                ResetSeat(item);
            }
        }

        private void BtnRemoveSeat_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                if (button.DataContext is Seat seat)
                {
                    SelectedSeats.Remove(seat);
                }
            }
        }

        private void HyperlinkRemoveAll_Click(object sender, RoutedEventArgs e)
        {
            for (int i = SelectedSeats.Count - 1; i >= 0; i--)
            {
                SelectedSeats.RemoveAt(i);
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            frmBookingTicket.BtnNext_Click(sender, e);
        }
    }
}
