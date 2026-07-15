using System;
using System.Collections.Generic;
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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AnhQuoc_WPF_C1_B1.Views;
using AnhQuoc_WPF_C1_B1.UserControls.Admin;

namespace AnhQuoc_WPF_C1_B1
{
    /// <summary>
    /// Interaction logic for ucMovieScheduleTable.xaml
    /// </summary>
    public partial class ucMovieScheduleTable : UserControl, INotifyPropertyChanged
    {
        #region Properties
        public Func<RepositoryBase<MovieSchedule>> getMovieScheduleRepo { get; set; }
        public Func<RepositoryBase<Movie>> getMovieRepo { get; set; }
        public Func<RepositoryBase<Cinema>> getCinemaRepo { get; set; }
        public Func<frmAdmin> getFrmAdmin { get; set; }
        #endregion

        public object thisContent;
        private MovieScheduleViewModel movieScheduleVM;

        public Func<string> getFileSeat { get; set; }
        private ObservableCollection<Movie> movieObs { get; set; }

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        public ucMovieScheduleTable()
        {
            InitializeComponent();
            movieScheduleVM = new MovieScheduleViewModel();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (getMovieScheduleRepo == null)
                return;
            movieScheduleVM.MovieScheduleRepo = getMovieScheduleRepo();

            List<Movie> movies = movieScheduleVM.FillMovie();
            movieObs = new ObservableCollection<Movie>(movies);
            dgTable.ItemsSource = movieObs;
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            ucMovieInformation ucMovieInformation = new ucMovieInformation();
            ucMovieInformation.GetMovie = dgTable.SelectedItem as Movie;

            Window frmMovieInformation = new Window();
            frmMovieInformation.Content = ucMovieInformation;
            frmMovieInformation.Show();
        }

        private void LoadUcMoviePicker(string feature)
        {
            List<Movie> moviesInSchedule = movieScheduleVM.FillMovie();
            MovieViewModel movieVM = new MovieViewModel();
            movieVM.MovieRepo = getMovieRepo();

            List<Movie> fillMovie = movieVM.FillByList(moviesInSchedule);
            if (fillMovie.Count == 0)
            {
                MessageBox.Show(Utilities.GetListEmptyMessage("Movie"));
                return;
            }
            ucMoviePickerTable ucMoviePickerTable = new ucMoviePickerTable();
            ucMoviePickerTable.getMovieRepo = () => new RepositoryBase<Movie>(fillMovie);

            if (feature == "update")
            {
                MovieSchedule getMovieSchedule = movieScheduleVM.GetByMovie(dgTable.SelectedItem as Movie);
                ucMoviePickerTable.getCurrentMovieSchedule = () => getMovieSchedule;
            }
            ucMoviePickerTable.getFeature = () => feature;
            ucMoviePickerTable.getUcMovieScheduleTable = () => this;
            thisContent = this.Content;
            this.Content = ucMoviePickerTable;
        }

        public void AddData(Movie newMovie)
        {
            this.Content = thisContent;
            MovieSchedule newItem = new MovieSchedule
            {
                Movie = newMovie,
                CinemaTypeSchedules = new List<CinemaTypeSchedule>()
            };
            movieObs.Add(newItem.Movie);
            movieScheduleVM.MovieScheduleRepo.Add(newItem);

            string fileSeat = movieScheduleVM.CreateFileSeatName(newItem, getFileSeat());
            Utilities.CreateDirectory(fileSeat);
        }

        public void UpdateData(MovieSchedule currentItem, Movie inputMovie)
        {
            this.Content = thisContent;

            Movie oldMovie = currentItem.Movie;
            movieObs.Insert(movieObs.IndexOf(oldMovie), inputMovie);
            movieObs.Remove(oldMovie);

            string oldFileSeat = movieScheduleVM.CreateFileSeatName(currentItem, getFileSeat());
            currentItem.Movie = inputMovie;
            string newFileSeat = movieScheduleVM.CreateFileSeatName(currentItem, getFileSeat());

            movieScheduleVM.WriteUpdateData(oldMovie, inputMovie);
            Utilities.RenameDirectory(oldFileSeat, newFileSeat);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            LoadUcMoviePicker("add");
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msbResult = MessageBox.Show("Are you sure you want to delete this item?", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning, MessageBoxResult.Cancel);
            if (msbResult == MessageBoxResult.Cancel)
                return;
            Movie selectedItem = dgTable.SelectedItem as Movie;
            Btn_Delete(selectedItem);
        }

        public void Btn_Delete(Movie item)
        {
           //
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            ucMovieInformation ucMovieInformation = new ucMovieInformation();
            ucMovieInformation.GetMovie = dgTable.SelectedItem as Movie;
            ucMovieInformation.IsReadOnly = false;
            Window frmMovieInformation = new Window();
            frmMovieInformation.Content = ucMovieInformation;
            frmMovieInformation.Show();
            //Movie getMovie = dgTable.SelectedItem as Movie;
            //MovieSchedule newMovieSchedule = movieScheduleVM.GetByMovie(getMovie);

            //if (newMovieSchedule == null)
            //{
            //    Utilities.HandleError();
            //}
            //string fileSeat = movieScheduleVM.CreateFileSeatName(newMovieSchedule, getFileSeat());

            //ucCinemaManage ucCinemaManage = new ucCinemaManage();
            //ucCinemaManage.getFileSeat = () => fileSeat;
            //ucCinemaManage.getMovieSchedule = () => newMovieSchedule;


            //ucCinemaManage.getFrmAdmin = getFrmAdmin;
            //ucCinemaManage.getCinemaTypeSchedules = () => newMovieSchedule.CinemaTypeSchedules;

            //EnumViewModel enumVM = new EnumViewModel();
            //ucCinemaManage.getAllCinemaTypes = () => enumVM.GetValues<CinemaType>().ToList();
            //ucCinemaManage.getCinemaRepo = getCinemaRepo;

            //getFrmAdmin().stkView.Children.Clear();
            //getFrmAdmin().stkView.Children.Add(ucCinemaManage);


            //LoadUcMoviePicker("update");
        }

        public void DeleteMovieSchedule(Movie item)
        {
            MovieScheduleViewModel movieScheduleViewModel = new MovieScheduleViewModel();
            movieScheduleViewModel.MovieScheduleRepo = getMovieScheduleRepo();
            MovieSchedule newItem = movieScheduleViewModel.GetByMovie(item);
            if (newItem == null) return;

            movieScheduleViewModel.MovieScheduleRepo.Remove(newItem);
            movieScheduleViewModel.WriteRemoveData(newItem);

            string fileSeat = movieScheduleViewModel.CreateFileSeatName(newItem, getFileSeat());
            Utilities.DeleteDirectory(fileSeat);
        }
    }
}
