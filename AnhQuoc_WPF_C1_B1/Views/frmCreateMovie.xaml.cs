using AutoMapper;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
using WPFCustomMessageBox;
using static System.Net.WebRequestMethods;

namespace AnhQuoc_WPF_C1_B1
{
    /// <summary>
    /// Interaction logic for frmCreateMovie.xaml
    /// </summary>
    public partial class frmCreateMovie : Window, INotifyPropertyChanged
    {
        #region Feilds
        private Movie _movieCopy;
        #endregion

        public bool frmReply;
        public Func<string> optionFrm;

        public Func<ucMovieTable> getUcMovieTable;

        public Func<RepositoryBase<Movie>> getMovieRepo { get; set; }
        public Func<RepositoryBase<Genre>> getGenreRepo { get; set; }

        public Func<List<Genre>> getGenres;
        public Func<List<Rated>> getRateds;

        private ImageSource _UrlImage;
        public ImageSource UrlImage
        {
            get
            {
                return _UrlImage;
            }
            set
            {
                _UrlImage = value;
                OnPropertyChanged("UrlImage");
            }
        }

        private Movie _Movie;
        public Movie Movie
        {
            get { return _Movie; }
            set { 
                _Movie = value;
                OnPropertyChanged();
            }
        }


        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        public frmCreateMovie()
        {
            InitializeComponent();
            
            frmReply = false;
            this.DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (optionFrm() == "update")
            {
                _movieCopy = new Movie();
                Utilities.CopyProperties(Movie, _movieCopy);
            }
            if (optionFrm == null)
                return;
            ResetValues();
            this.DataContext = this;
        }

        public void ResetValues()
        {
            if (optionFrm() == "add")
                Movie = new Movie();
            if (Movie == null)
                return;
            cbRateds.ItemsSource = null;
            LoadRateds();

            stkGenres.Children.Clear();
            List<CheckBox> checkBoxs = AddCheckBox(getGenres());

            if (optionFrm() == "update")
            {
                this.Title = "Update new movie";
                lblHeader.Content = this.Title;
            }
            else
            {
                this.Title = "Add new movie";
                lblHeader.Content = this.Title;
            }
        }
        
        private void LoadRateds()
        {
            ObservableCollection<Rated> _rateds = new ObservableCollection<Rated>(getRateds());
            cbRateds.ItemsSource = _rateds;
            cbRateds.DisplayMemberPath = "Id";

            if (_rateds.Count > 0)
                cbRateds.SelectedIndex = 0;
        }

        public List<Genre> GetCheckedGenres()
        {
            GenreViewModel genreVM = new GenreViewModel();
            genreVM.GenreRepo = getGenreRepo();

            List<Genre> getGenres = new List<Genre>();
            foreach (CheckBox cb in GetCheckBox())
            {
                string idGenre = cb.Name;
                Genre getGenre = genreVM.FindById(idGenre);

                getGenres.Add(getGenre);
            }
            return getGenres;
        }

        private List<CheckBox> AddCheckBox(List<Genre> items)
        {
            List<CheckBox> checkBoxs = new List<CheckBox>();
            foreach (var item in items)
            {
                CheckBox cb = new CheckBox();
                cb.Name = item.Id;
                cb.Width = 100;
                cb.Content = item.Name;
                cb.FontSize = 12;
                stkGenres.Children.Add(cb);
                checkBoxs.Add(cb);
            }
            return checkBoxs;
        }

        private void CheckedCheckBox(List<Genre> items, List<CheckBox> checkBoxs)
        {
            foreach (var item in items)
            {
                int indexOf = getGenres().IndexOf(item);
                CheckBox cb = checkBoxs[indexOf];
                cb.IsChecked = true;
            }
        }

        public List<CheckBox> GetCheckBox()
        {
            List<CheckBox> cbCheckeds = new List<CheckBox>();
            foreach (var control in stkGenres.Children)
            {
                CheckBox cb = null;
                if (control is CheckBox)
                    cb = control as CheckBox;
                else
                    continue;
                bool? check = cb.IsChecked;
                if (check != null && check == true)
                    cbCheckeds.Add(cb);
            }
            return cbCheckeds;
        }

        private void btnUploadImage_Click(object sender, RoutedEventArgs e)
        {
            string fileName = null;
            UrlImage = Utilities.SelectImage("Movies", ref fileName);
            if (UrlImage == null)
                return;
            Movie.UrlImage = fileName;
        }

        private bool Checking()
        {
            if (Utilities.IsEmpty(txtName.Text))
            {
                MessageBox.Show($"Please enter the {"Movie name"}", string.Empty, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                return false;
            }

            // Check genres
            if (dateReleaseDate.SelectedDate == null)
            {
                MessageBox.Show($"Please enter the {"Movie Release Date"}", string.Empty, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                return false;
            }
            try
            {
                int runningTime = Convert.ToInt32(txtRunningTime.Text);
                if (runningTime == 0)
                {
                    MessageBox.Show($"Please enter the {"Movie Running Time"}", string.Empty, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, string.Empty, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                return false;
            }

            List<Genre> genres = GetCheckedGenres();
            Rated rated = cbRateds.SelectedItem as Rated;

            if (genres.Count == 0)
            {
                MessageBox.Show($"Please select the {"Movie genres"}", string.Empty, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                return false;
            }

            if (rated == null)
            {
                MessageBox.Show($"Please pick the {"movie rated"}", string.Empty, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                return false;
            }
            return true;
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (!Checking())
                return;

            MovieViewModel movieViewModel = new MovieViewModel();
            movieViewModel.MovieRepo = getMovieRepo();

            Movie movieFinded = movieViewModel.FindByName(Movie.Name);
            if (movieFinded != null)
            {
                MessageBox.Show("This movie was already on the list.", "Warning", MessageBoxButton.OK);
                return;
            }    

            List<Genre> genres = GetCheckedGenres();

            int length = getMovieRepo().Length();
            MovieViewModel movieVM = new MovieViewModel();

            Movie.Id = movieVM.GetId(length);
            Movie.Genres = genres;

            frmReply = true;
            this.Close();
        }
        

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            frmReply = false;
            this.Close();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            Utilities.FeatureNotDevelopNotify();
        }
    }
}
