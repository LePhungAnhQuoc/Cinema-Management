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

namespace AnhQuoc_WPF_C1_B1
{
    /// <summary>
    /// Interaction logic for ucTimeScheduleTable.xaml
    /// </summary>
    public partial class ucTimeScheduleTable : UserControl
    {
        public Func<ucCinemaManage> getUcCinemaManage { get; set; }
        public Func<MovieSchedule> getMovieSchedule { get; set; }
        public Func<CinemaTypeSchedule> getCinemaTypeSchedule { get; set; }
        public Func<CinemaSchedule> getCinemaSchedule { get; set; }
        public Func<DateSchedule> getDateSchedule { get; set; }

        private Func<List<TimeSchedule>> _getTimeSchedules;
        public Func<List<TimeSchedule>> getTimeSchedules
        {
            get { return _getTimeSchedules; }
            set
            {
                _getTimeSchedules = value;
                OnChangeDateSchedule();
            }
        }
        public Func<string> getFileSeat { get; set; }

        private TimeScheduleViewModel timeScheduleVM { get; set; }
        private ObservableCollection<TimeSchedule> GetSource;

        public TimeSchedule currentItem;
        public ucTimeScheduleTable()
        {
            InitializeComponent();
            timeScheduleVM = new TimeScheduleViewModel();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (GetSource.Count > 0)
                dgTable.SelectedIndex = 0;
        }

        private void OnChangeDateSchedule()
        {
            GetSource = new ObservableCollection<TimeSchedule>(getTimeSchedules());
            dgTable.ItemsSource = GetSource;
        }

        private void LoadUcTimePicker(string feature)
        {
            frmAddTime frmAddTime = new frmAddTime();
            if (feature == "update")
            {
                currentItem = dgTable.SelectedItem as TimeSchedule;
            }
            frmAddTime.getFeature = () => feature;
            frmAddTime.getUcTimeScheduleTable = () => this;
            frmAddTime.getMovieRunningTimes = () => getMovieSchedule().Movie.RunningTime;

            frmAddTime.Show();
        }

        public void AddData(TimeSpan data)
        {
            TimeSchedule newItem = new TimeSchedule
            {
                Time = data,
                Seats = new List<List<Seat>>(),
            };
            timeScheduleVM.TimeScheduleRepo.Items.Add(newItem);
            GetSource.Add(newItem);
            getTimeSchedules().Add(newItem);

            string fileSeat = timeScheduleVM.CreateFileSeatName(newItem.Time, getFileSeat());
            Utilities.CreateXML(fileSeat, "Seats");

            WriteSeat(newItem, fileSeat);

            timeScheduleVM.WriteData(getMovieSchedule().Movie,
               getCinemaTypeSchedule().CinemaType, getCinemaSchedule().Cinema,
               getDateSchedule().Date, newItem);
        }

        public void UpdateData(TimeSpan data)
        {
            TimeSpan oldValue = currentItem.Time;
            currentItem.Time = data;

            string oldFileSeat = timeScheduleVM.CreateFileSeatName(oldValue, getFileSeat());
            string newFileSeat = timeScheduleVM.CreateFileSeatName(data, getFileSeat());
            Utilities.RenameFile(oldFileSeat, newFileSeat);

            //WriteSeat(newItem);
            timeScheduleVM.WriteUpdateData(getMovieSchedule().Movie,
               getCinemaTypeSchedule().CinemaType, getCinemaSchedule().Cinema,
               getDateSchedule().Date, oldValue, data);
        }

        private void WriteSeat(TimeSchedule timeSchedule, string fileSeat)
        {
            SeatViewModel seatVM = new SeatViewModel();
            timeSchedule.Seats = getCinemaSchedule().Cinema.Seats;
            seatVM.WriteListSeat(timeSchedule.Seats, fileSeat);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            LoadUcTimePicker("add");
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msbResult = MessageBox.Show("Do you want to remove this item", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning, MessageBoxResult.Cancel);
            if (msbResult == MessageBoxResult.Cancel)
                return;
            TimeSchedule newItem = null;
            try
            {
                newItem = (TimeSchedule)dgTable.SelectedItem;
            }
            catch
            {
                Utilities.HandleError();
            }
            GetSource.Remove(newItem);
            getTimeSchedules().Remove(newItem);
            timeScheduleVM.TimeScheduleRepo.Remove(newItem);
            timeScheduleVM.WriteRemoveData(getMovieSchedule().Movie, getCinemaTypeSchedule().CinemaType, getCinemaSchedule().Cinema, getDateSchedule().Date, newItem);

            string fileSeat = timeScheduleVM.CreateFileSeatName(newItem.Time, getFileSeat());
            Utilities.DeleteFile(fileSeat);
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            LoadUcTimePicker("update");
        }
    }
}
