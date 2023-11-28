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
    /// Interaction logic for ucCinemaScheduleTable.xaml
    /// </summary>
    public partial class ucCinemaScheduleTable : UserControl, INotifyPropertyChanged
    {
        public Func<string> getFileSeat { get; set; }
        public Func<MovieSchedule> getMovieSchedule { get; set; }
        public Func<CinemaTypeSchedule> getCinemaTypeSchedule { get; set; }

        private Func<List<CinemaSchedule>> _getCinemaSchedule;
        public Func<List<CinemaSchedule>> getCinemaSchedule
        {
            get { return _getCinemaSchedule; }
            set
            {
                _getCinemaSchedule = value;
                UserControl_Loaded(null, null);
            }
        }
     
        public Func<RepositoryBase<Cinema>> getCinemaRepo { get; set; }
        public Func<ucCinemaManage> getUcCinemaManage { get; set; }

        private CinemaScheduleViewModel cinemaScheduleVM;

        public ObservableCollection<Cinema> GetSource { get; set; }
        private ucDateScheduleTable ucDateSchedule;
        private object thisContent;

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        private CinemaSchedule CurrentCinemaSchedule;
        public ucCinemaScheduleTable()
        {
            InitializeComponent();
            cinemaScheduleVM = new CinemaScheduleViewModel();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ucDateSchedule = getUcCinemaManage().ucDateSchedule;
            cinemaScheduleVM.CinemaScheduleRepo.Items = getCinemaSchedule();
            List<Cinema> cinemas = cinemaScheduleVM.FillCinema();

            GetSource = new ObservableCollection<Cinema>(cinemas);
            dgTable.ItemsSource = GetSource;
            dgTable.SelectedIndex = 0;
        }
    
        public void AddData(Cinema data)
        {
            this.Content = thisContent;
            CinemaSchedule newItem = new CinemaSchedule
            {
                Cinema = data,
                DatesSchedule = new List<DateSchedule>(),
            };
            cinemaScheduleVM.CinemaScheduleRepo.Items.Add(newItem);
            GetSource.Add(newItem.Cinema);

            cinemaScheduleVM.WriteData(getMovieSchedule().Movie, getCinemaTypeSchedule().CinemaType, newItem);
            string fileSeat = cinemaScheduleVM.CreateFileSeatName(newItem, getFileSeat());
            Utilities.CreateDirectory(fileSeat);
        }

        public void UpdateData(Cinema data)
        {
            this.Content = thisContent;
            Cinema oldData = CurrentCinemaSchedule.Cinema;

            GetSource.Insert(GetSource.IndexOf(oldData), data);
            GetSource.Remove(oldData);

            cinemaScheduleVM.WriteUpdateData(getMovieSchedule().Movie, getCinemaTypeSchedule().CinemaType, oldData, data);

            string oldFileSeat = cinemaScheduleVM.CreateFileSeatName(CurrentCinemaSchedule, getFileSeat());
            CurrentCinemaSchedule.Cinema = data;
            string newFileSeat = cinemaScheduleVM.CreateFileSeatName(CurrentCinemaSchedule, getFileSeat());
            Utilities.RenameDirectory(oldFileSeat, newFileSeat);
        }

        private void LoadUcCinemaPicker(string feature)
        {
            List<Cinema> cinemas = cinemaScheduleVM.FillCinema();

            CinemaViewModel cinemaVM = new CinemaViewModel();
            cinemaVM.CinemaRepo = getCinemaRepo();
            List<Cinema> filledCinemas = cinemaVM.FillByList(cinemas);
            
            frmAddCinema frmAddCinema = new frmAddCinema();
            frmAddCinema.getCinemaRepo = () => new RepositoryBase<Cinema>(filledCinemas);
            frmAddCinema.getUcCinemaScheduleTable = () => this;

            CinemaViewModel cinemaVM2 = new CinemaViewModel();
            cinemaVM2.CinemaRepo.Items = filledCinemas;

            List<Cinema> lastFilled = cinemaVM2.FillByType(getCinemaTypeSchedule().CinemaType);
            if (lastFilled.Count == 0) 
            {
                MessageBox.Show(Utilities.GetListEmptyMessage("Cinema"));
                return;
            }

            if (feature == "update")
            {
                Cinema select = dgTable.SelectedItem as Cinema;
                CurrentCinemaSchedule = cinemaScheduleVM.GetByCinema(select);
            }
            frmAddCinema.getFeature = () => feature;
            frmAddCinema.getCinemaRepo = () => new RepositoryBase<Cinema>(lastFilled);

            thisContent = this.Content;

            frmAddCinema.ShowDialog();
        }
        
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            LoadUcCinemaPicker("add");
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msbResult = MessageBox.Show("Do you want to remove this item", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning, MessageBoxResult.Cancel);
            if (msbResult == MessageBoxResult.Cancel)
                return;
            Cinema selectedItem = null;
            try
            {
                selectedItem = (Cinema)dgTable.SelectedItem;
            }
            catch
            {
                Utilities.HandleError();
            }
            CinemaSchedule newItem = cinemaScheduleVM.GetByCinema(selectedItem);

            GetSource.Remove(newItem.Cinema);
            cinemaScheduleVM.CinemaScheduleRepo.Remove(newItem);

            cinemaScheduleVM.WriteRemoveData(getMovieSchedule().Movie, getCinemaTypeSchedule().CinemaType, newItem);
            string fileSeat = cinemaScheduleVM.CreateFileSeatName(newItem, getFileSeat());
            Utilities.DeleteDirectory(fileSeat);
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            LoadUcCinemaPicker("update");
        }
        
        private void dgTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Cinema selectCinema = dgTable.SelectedItem as Cinema;
            if (selectCinema == null)
            {
                ucDateSchedule.getDateSchedules = () => new List<DateSchedule>();
                return;
            }
            getUcCinemaManage().lblCinemaInfo.Content = $"Cinema Choose: {selectCinema.Name}";
            CinemaSchedule cinemaSchedule = cinemaScheduleVM.GetByCinema(selectCinema);

            string fileSeat = cinemaScheduleVM.CreateFileSeatName(cinemaSchedule, getFileSeat());
            ucDateSchedule.getFileSeat = () => fileSeat;

            ucDateSchedule.getUcCinemaManage = getUcCinemaManage;
            ucDateSchedule.getMovieSchedule = getMovieSchedule;
            ucDateSchedule.getCinemaTypeSchedule = getCinemaTypeSchedule;
            ucDateSchedule.getCinemaSchedule = () => cinemaSchedule;
            ucDateSchedule.getDateSchedules = () => cinemaSchedule.DatesSchedule;
        }
    }
}
