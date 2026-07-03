using AnhQuoc_WPF_C1_B1.UserControls.Admin;
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
using System.Xml;

namespace AnhQuoc_WPF_C1_B1
{
    /// <summary>
    /// Interaction logic for ucMovieTable.xaml
    /// </summary>
    public partial class ucMovieTable : UserControl, INotifyPropertyChanged
    {
        public Func<RepositoryBase<Movie>> getMovieRepo { get; set; }
        public Func<RepositoryBase<Genre>> getGenreRepo { get; set; }
        public Func<RepositoryBase<Rated>> getRatedRepo { get; set; }
        public Func<RepositoryBase<MovieSchedule>> getMovieScheduleRepo { get; set; }

        public Func<frmAdmin> getFrmAdmin { get; set; }

        private ObservableCollection<Movie> MoviesOb;

        #region Properties
        private Movie _CurrentItem;
        public Movie CurrentItem
        {
            get { return _CurrentItem; }
            set 
            { 
                _CurrentItem = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
        public ucMovieTable()
        {
            InitializeComponent();
            MoviesOb = new ObservableCollection<Movie>();
            this.DataContext = this;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            List<Movie> movies = null;
            if (getMovieRepo != null)
            {
                movies = getMovieRepo().Gets();
            }
            else
            {
                MessageBox.Show("Error unsolve");
                return;
            }
            MoviesOb = new ObservableCollection<Movie>(movies);
            try
            {
                dgTable.ItemsSource = MoviesOb;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddData(Movie newMovie)
        {
            MovieViewModel movieVM = new MovieViewModel();
            
            // Add to list
            getMovieRepo().Add(newMovie);

            // Add to database
            DataProvider.Instance.pathData = Constants.fMovies;
            DataProvider.Instance.Open();
            DataProvider.Instance.nodeRoot.AppendChild(movieVM.Write(newMovie));
            DataProvider.Instance.Close();

            // Add to movie detail (Database)
            DataProvider.Instance.pathData = Constants.fMovieDetails;
            DataProvider.Instance.Open();
            DataProvider.Instance.nodeRoot.AppendChild(movieVM.WriteDetail(newMovie));
            DataProvider.Instance.Close();

            // Add
            this.MoviesOb.Add(newMovie);
        }

        private void DeleteData(int indexOf)
        {
            MovieViewModel movieVM = new MovieViewModel();
            getMovieRepo().Items.RemoveAt(indexOf);

            DataProvider.Instance.pathData = Constants.fMovies;
            DataProvider.Instance.Open();
            XmlNode child = DataProvider.Instance.nodeRoot.ChildNodes[indexOf];
            DataProvider.Instance.nodeRoot.RemoveChild(child);
            DataProvider.Instance.Close();

            DataProvider.Instance.pathData = Constants.fMovieDetails;
            DataProvider.Instance.Open();
            child = DataProvider.Instance.nodeRoot.ChildNodes[indexOf];
            DataProvider.Instance.nodeRoot.RemoveChild(child);
            DataProvider.Instance.Close();
        }

        private void UpdateData(int indexOf, Movie inputMovie)
        {
            MovieViewModel movieVM = new MovieViewModel();

            // Update to list
            getMovieRepo().Items[indexOf] = inputMovie;

            // Update to database
            DataProvider.Instance.pathData = Constants.fMovies;
            DataProvider.Instance.Open();

            XmlNode newNode = movieVM.Write(inputMovie);
            XmlNode oldNode = DataProvider.Instance.nodeRoot.ChildNodes[indexOf];
            DataProvider.Instance.nodeRoot.ReplaceChild(newNode, oldNode);
            
            DataProvider.Instance.Close();

            // Update to movie detail (Database)
            DataProvider.Instance.pathData = Constants.fMovieDetails;
            DataProvider.Instance.Open();

            newNode = movieVM.WriteDetail(inputMovie);
            oldNode = DataProvider.Instance.nodeRoot.ChildNodes[indexOf];
            DataProvider.Instance.nodeRoot.ReplaceChild(newNode, oldNode);
            DataProvider.Instance.Close();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            frmCreateMovie frmCreateMovie = new frmCreateMovie();
            frmCreateMovie.optionFrm = () => "add";
            frmCreateMovie.getGenres = () => getGenreRepo().Gets();
            frmCreateMovie.getRateds = () => getRatedRepo().Gets();

            frmCreateMovie.getMovieRepo = getMovieRepo;
            frmCreateMovie.getGenreRepo = getGenreRepo;

            frmCreateMovie.ShowDialog();

            if (frmCreateMovie.frmReply)
            {
                AddData(frmCreateMovie.Movie);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            frmDeleteMovie frmDeleteMovie = new frmDeleteMovie();
            frmDeleteMovie.Movie = CurrentItem;
            frmDeleteMovie.ShowDialog();

            if (frmDeleteMovie.DialogResult == true)
            {
                int currentRowIndex = dgTable.Items.IndexOf(dgTable.CurrentItem);
                getFrmAdmin().getDeleteMovie = () => getMovieRepo().GetByIndex(currentRowIndex);
                getFrmAdmin().DeleteMovieSchedule();

                MoviesOb.RemoveAt(currentRowIndex);

                DeleteData(currentRowIndex);
            }
        }
                
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            int currentRowIndex = dgTable.Items.IndexOf(dgTable.CurrentItem);

            frmCreateMovie frmCreateMovie = new frmCreateMovie();
            frmCreateMovie.optionFrm = () => "update";
            frmCreateMovie.Movie = CurrentItem;
            frmCreateMovie.getGenres = () => getGenreRepo().Gets();
            frmCreateMovie.getRateds = () => getRatedRepo().Gets();

            frmCreateMovie.getMovieRepo = getMovieRepo;
            frmCreateMovie.getGenreRepo = getGenreRepo;

            frmCreateMovie.ShowDialog();

            if (frmCreateMovie.frmReply)
            {
                UpdateData(currentRowIndex, CurrentItem);
            }
        }
    }
}
