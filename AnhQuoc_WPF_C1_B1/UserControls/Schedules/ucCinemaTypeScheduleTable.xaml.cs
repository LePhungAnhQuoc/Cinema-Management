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
    /// Interaction logic for ucCinemaTypeScheduleTable.xaml
    /// </summary>
    public partial class ucCinemaTypeScheduleTable : UserControl
    {
        public Func<string> getFileSeat { get; set; }
        public Func<MovieSchedule> getMovieSchedule { get; set; }

        public Func<List<CinemaTypeSchedule>> getCinemaTypeSchedule { get; set; }
        public Func<List<CinemaType>> getAllCinemaTypes { get; set; }
        public Func<RepositoryBase<Cinema>> getCinemaRepo { get; set; }
        public Func<ucCinemaManage> getUcCinemaManage { get; set; }

        private CinemaTypeScheduleViewModel cinemaTypeScheduleVM;
        private object thisContent;

        private ObservableCollection<CinemaType> _CinemaTypes;
        public ObservableCollection<CinemaType> CinemaTypes
        {
            get { return _CinemaTypes; }
            set
            { 
                _CinemaTypes = value;
                OnPropertyChanged();
            }
        }

        private CinemaType _CurrentItem;
        public CinemaType CurrentItem
        {
            get { return _CurrentItem; }
            set 
            { 
                _CurrentItem = value;
                OnPropertyChanged();
            }
        }
        private ucCinemaScheduleTable ucCinemaSchedule;

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion



        public ucCinemaTypeScheduleTable()
        {
            InitializeComponent();
            this.DataContext = this;

            cinemaTypeScheduleVM = new CinemaTypeScheduleViewModel();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ucCinemaSchedule = getUcCinemaManage().ucCinemaSchedule;
            cinemaTypeScheduleVM.Repo.Items = getCinemaTypeSchedule();
            List<CinemaType> cinemaTypes = cinemaTypeScheduleVM.FillCinemaType();

            CinemaTypes = new ObservableCollection<CinemaType>(cinemaTypes);
            cbCinemaTypes.ItemsSource = CinemaTypes;

            if (CinemaTypes.Count > 0)
                cbCinemaTypes.SelectedIndex = 0;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msbResult = MessageBox.Show("Do you want to remove this item", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning, MessageBoxResult.Cancel);
            if (msbResult == MessageBoxResult.Cancel)
                return;
            CinemaType selectedItem = (CinemaType)cbCinemaTypes.SelectedItem;
            CinemaTypeSchedule newItem = cinemaTypeScheduleVM.GetByCinemaType(selectedItem);
            cinemaTypeScheduleVM.Repo.Remove(newItem);

            CinemaTypes.Remove(newItem.CinemaType);

            cinemaTypeScheduleVM.WriteRemoveData(getMovieSchedule().Movie, newItem);

            string fileSeat = cinemaTypeScheduleVM.CreateFileSeatName(newItem, getFileSeat());
            Utilities.DeleteDirectory(fileSeat);
        }
        
        public void AddData(CinemaType data)
        {
            CinemaTypeSchedule newCinemaTypeSchedule = new CinemaTypeSchedule
            {
                CinemaType = data,
                CinemaSchedules = new List<CinemaSchedule>(),
            };
            cinemaTypeScheduleVM.Repo.Items.Add(newCinemaTypeSchedule);
            CinemaTypes.Add(newCinemaTypeSchedule.CinemaType);

            cinemaTypeScheduleVM.WriteData(getMovieSchedule().Movie, newCinemaTypeSchedule);

            string fileSeat = cinemaTypeScheduleVM.CreateFileSeatName(newCinemaTypeSchedule, getFileSeat());
            Utilities.CreateDirectory(fileSeat);
        }

        private void LoadFrmAddCinemaType()
        {
            List<CinemaType> cinemaTypes = cinemaTypeScheduleVM.FillCinemaType();
            CinemaTypeViewModel cinemaTypeVM = new CinemaTypeViewModel();
            cinemaTypeVM.CinemaTypeRepo = new EnumViewModel().GetValues<CinemaType>().ToList();
            List<CinemaType> filledCinemaTypes = cinemaTypeVM.FillByList(cinemaTypes);

            if (filledCinemaTypes.Count == 0)
            {
                MessageBox.Show(Utilities.GetListEmptyMessage("Cinema Type"));
                return;
            }
            frmAddCinemaType frmAddCinemaTypeTable = new frmAddCinemaType();
            frmAddCinemaTypeTable.getCinemaTypeRepo = () => filledCinemaTypes;
            frmAddCinemaTypeTable.getUcCinemaTypeScheduleTable = () => this;

            thisContent = this.Content;
            frmAddCinemaTypeTable.Show();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            LoadFrmAddCinemaType();
        }

        private void cbCinemaTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CinemaType selectCinemaType;
            if (cbCinemaTypes.SelectedItem == null)
            {
                ucCinemaSchedule.IsEnabled = false;
                ucCinemaSchedule.getCinemaSchedule = () => new List<CinemaSchedule>();
                return;
            }
            selectCinemaType = (CinemaType)cbCinemaTypes.SelectedItem;

            CinemaTypeSchedule cinemaTypeSchedule = cinemaTypeScheduleVM.GetByCinemaType(selectCinemaType);
            if (cinemaTypeSchedule == null) return;

            ucCinemaSchedule.IsEnabled = true;
            string fileSeat = cinemaTypeScheduleVM.CreateFileSeatName(cinemaTypeSchedule, getFileSeat());

            ucCinemaSchedule.getFileSeat = () => fileSeat;
            ucCinemaSchedule.getUcCinemaManage = getUcCinemaManage;

            ucCinemaSchedule.getMovieSchedule = getMovieSchedule;
            ucCinemaSchedule.getCinemaTypeSchedule = () => cinemaTypeSchedule;
            ucCinemaSchedule.getCinemaRepo = getCinemaRepo;

            ucCinemaSchedule.getCinemaSchedule = () => cinemaTypeSchedule.CinemaSchedules;
        }
    }
}
