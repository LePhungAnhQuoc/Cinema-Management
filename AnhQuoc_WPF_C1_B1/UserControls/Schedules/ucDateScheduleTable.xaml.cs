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
    /// Interaction logic for ucDateScheduleTable.xaml
    /// </summary>
    public partial class ucDateScheduleTable : UserControl, INotifyPropertyChanged
    {
        public Func<string> getFileSeat { get; set; }

        public Func<ucCinemaManage> getUcCinemaManage { get; set; }
        public Func<MovieSchedule> getMovieSchedule { get; set; }
        public Func<CinemaTypeSchedule> getCinemaTypeSchedule { get; set; }
        public Func<CinemaSchedule> getCinemaSchedule { get; set; }

        private Func<List<DateSchedule>> _getDateSchedules;
        public Func<List<DateSchedule>> getDateSchedules
        {
            get { return _getDateSchedules; }
            set
            {
                _getDateSchedules = value;
                DateSchedules = new ObservableCollection<DateSchedule>(value());
            }
        }

        private ObservableCollection<DateSchedule> _DateSchedules;
        public ObservableCollection<DateSchedule> DateSchedules
        {
            get { return _DateSchedules; }
            set
            {
                _DateSchedules = value;
                OnPropertyChanged();
            }
        }

        private DateScheduleViewModel dateScheduleVM;

        private DateSchedule currentItem;
        public DateSchedule CurrentItem
        {
            get { return currentItem; }
            set
            {
                currentItem = value;
                OnPropertyChanged("CurrentItem");
            }
        }
        private object thisContent;

        private ucTimeScheduleTable ucTimeSchedule;
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        public ucDateScheduleTable()
        {
            InitializeComponent();
            this.DataContext = this;
            dateScheduleVM = new DateScheduleViewModel();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ucTimeSchedule = getUcCinemaManage().ucTimeSchedule;

            if (DateSchedules.Count > 0)
                cbDates.SelectedIndex = 0;
        }

        private List<string> ConvertDateSource(List<DateTime> dates)
        {
            List<string> datesStr = new List<string>();
            foreach (DateTime date in dates)
            {
                datesStr.Add(date.ToShortDateString());
            }
            return datesStr;
        }

        private void LoadFrmAddDate(string feature)
        {
            frmAddDate frmAddDate = new frmAddDate();
            frmAddDate.getFeature = () => feature;
            frmAddDate.getUcDateScheduleTable = () => this;
            thisContent = this.Content;
            //this.stkTable.Children.Remove(stkAddBtn);
            //this.stkTable.Children.Add(frmAddDate);
            frmAddDate.ShowDialog();
        }

        public void AddData(DateTime data)
        {
            DateSchedule newItem = new DateSchedule
            {
                Date = data,
                TimeShedules = new List<TimeSchedule>(),
            };
            dateScheduleVM.DateScheduleRepo.Items.Add(newItem);
            DateSchedules.Add(newItem);
            getDateSchedules().Add(newItem);

            dateScheduleVM.WriteData(getMovieSchedule().Movie, getCinemaTypeSchedule().CinemaType, getCinemaSchedule().Cinema, newItem);

            string fileSeat = dateScheduleVM.CreateFileSeatName(newItem.Date, getFileSeat());
            Utilities.CreateDirectory(fileSeat);
        }

        public void UpdateData(DateTime data)
        {
            DateTime oldDate = CurrentItem.Date;
            CurrentItem.Date = data;
            
            // GetSource.Insert(GetSource.IndexOf(oldDate), );

            dateScheduleVM.WriteUpdateData(getMovieSchedule().Movie, getCinemaTypeSchedule().CinemaType, getCinemaSchedule().Cinema, oldDate, data);
            string oldFileSeat = dateScheduleVM.CreateFileSeatName(oldDate, getFileSeat());
            string newFileSeat = dateScheduleVM.CreateFileSeatName(data, getFileSeat());
            Utilities.RenameDirectory(oldFileSeat, newFileSeat);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            LoadFrmAddDate("add");
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msbResult = MessageBox.Show("Do you want to remove this item", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning, MessageBoxResult.Cancel);
            if (msbResult == MessageBoxResult.Cancel)
                return;
            DateSchedule newItem = null;
            try
            {
                newItem = (DateSchedule)cbDates.SelectedItem;
            }
            catch
            {
                Utilities.HandleError();
            }
            DateSchedules.Remove(newItem);
            getDateSchedules().Remove(newItem);
           
            dateScheduleVM.WriteRemoveData(getMovieSchedule().Movie, getCinemaTypeSchedule().CinemaType, getCinemaSchedule().Cinema, newItem);

            string fileSeat = dateScheduleVM.CreateFileSeatName(newItem.Date, getFileSeat());
            Utilities.DeleteDirectory(fileSeat);
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            LoadFrmAddDate("update");
        }
        
        private void datePick_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void cbDates_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DateSchedule dateSchedule = cbDates.SelectedItem as DateSchedule;
            if (dateSchedule == null)
            {
                ucTimeSchedule.IsEnabled = false;
                ucTimeSchedule.getTimeSchedules = () => new List<TimeSchedule>();
                return;
            }
            string fileSeat = dateScheduleVM.CreateFileSeatName(dateSchedule.Date, getFileSeat());
            ucTimeSchedule.getFileSeat = () => fileSeat;

            ucTimeSchedule.IsEnabled = true;
            ucTimeSchedule.getUcCinemaManage = getUcCinemaManage;
            ucTimeSchedule.getMovieSchedule = getMovieSchedule;
            ucTimeSchedule.getCinemaTypeSchedule = getCinemaTypeSchedule;
            ucTimeSchedule.getCinemaSchedule = getCinemaSchedule;
            ucTimeSchedule.getDateSchedule = () => dateSchedule;
            ucTimeSchedule.getTimeSchedules = () => dateSchedule.TimeShedules;
        }
    }
}
