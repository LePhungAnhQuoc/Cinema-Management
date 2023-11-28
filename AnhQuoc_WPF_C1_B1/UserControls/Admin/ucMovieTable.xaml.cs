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
using System.Xml;

namespace AnhQuoc_WPF_C1_B1
{
    /// <summary>
    /// Interaction logic for ucMovieTable.xaml
    /// </summary>
    public partial class ucMovieTable : UserControl
    {
        public Func<RepositoryBase<Movie>> getMovieRepo { get; set; }
        public Func<RepositoryBase<Genre>> getGenreRepo { get; set; }
        public Func<RepositoryBase<Rated>> getRatedRepo { get; set; }

        public Func<frmAdmin> getFrmAdmin { get; set; }

        private ObservableCollection<Movie> MoviesOb;

        public ucMovieTable()
        {
            InitializeComponent();
            MoviesOb = new ObservableCollection<Movie>();
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
            if (getMovieRepo == null) return; 
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
            DataProvider.Instance.nodeRoot.AppendChild(child);
            DataProvider.Instance.Close();

            this.MoviesOb.RemoveAt(indexOf);
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

            // Update
            this.MoviesOb[indexOf] = inputMovie;
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
                Movie newMovie = frmCreateMovie.movie;
                //AddData(newMovie);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msgResult = MessageBox.Show("Are you sure you want to delete this movie and all the schedules?", "Are you sure", MessageBoxButton.OKCancel, MessageBoxImage.Warning, MessageBoxResult.Cancel);

            if (msgResult == MessageBoxResult.OK)
            {
                int currentRowIndex = dgTable.Items.IndexOf(dgTable.CurrentItem);
                MoviesOb.RemoveAt(currentRowIndex);

                getFrmAdmin().getDeleteMovie = () => getMovieRepo().GetByIndex(currentRowIndex);
                getFrmAdmin().DeleteMovieSchedule();
               
                DeleteData(currentRowIndex);
            }
        }
                
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            int currentRowIndex = dgTable.Items.IndexOf(dgTable.CurrentItem);
            Movie getMovie = MoviesOb[currentRowIndex];

            frmCreateMovie frmCreateMovie = new frmCreateMovie();
            frmCreateMovie.optionFrm = () => "update";
            frmCreateMovie.movie = getMovie;
            frmCreateMovie.getGenres = () => getGenreRepo().Gets();
            frmCreateMovie.getRateds = () => getRatedRepo().Gets();

            frmCreateMovie.getMovieRepo = getMovieRepo;
            frmCreateMovie.getGenreRepo = getGenreRepo;

            frmCreateMovie.ShowDialog();

            if (frmCreateMovie.frmReply)
            {
                Movie newMovie = frmCreateMovie.movie;
                //UpdateData(currentRowIndex, newMovie);
            }
        }
    }
}
