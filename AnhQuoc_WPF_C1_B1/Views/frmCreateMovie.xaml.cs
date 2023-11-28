using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Shapes;
using WPFCustomMessageBox;

namespace AnhQuoc_WPF_C1_B1
{
    /// <summary>
    /// Interaction logic for frmCreateMovie.xaml
    /// </summary>
    public partial class frmCreateMovie : Window, INotifyPropertyChanged
    {
        public bool frmReply;
        public Func<string> optionFrm;

        public Func<ucMovieTable> getUcMovieTable;

        public Func<RepositoryBase<Movie>> getMovieRepo { get; set; }
        public Func<RepositoryBase<Genre>> getGenreRepo { get; set; }

        public Func<List<Genre>> getGenres;
        public Func<List<Rated>> getRateds;

        #region BindingUrlImage
        private string _UrlImage;
        public string UrlImage
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
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public Movie movie { get; set; }

        public frmCreateMovie()
        {
            InitializeComponent();
            UrlImage = string.Empty;
            
            frmReply = false;
            this.DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (optionFrm == null)
                return;
            ResetValues();
        }

        public void ResetValues()
        {
            if (optionFrm() == "add")
                movie = new Movie();
            if (movie == null)
                return;
            cbRateds.ItemsSource = null;
            LoadRateds();

            stkGenres.Children.Clear();
            List<CheckBox> checkBoxs = AddCheckBox(getGenres());

            if (optionFrm() == "update")
            {
                this.Title = "Update new movie";
                lblHeader.Content = this.Title;

                txtName.Text = movie.Name;
                cbRateds.SelectedIndex = getRateds().IndexOf(movie.Rated);
                CheckedCheckBox(movie.Genres, checkBoxs);
                dateReleaseDate.SelectedDate = movie.ReleaseDate;
                txtRunningTime.Text = movie.RunningTime.ToString();
                txtUrlImage.Text = movie.UrlImage;
                txtDescription.Text = movie.Description;
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
            Image imgUrlImage = new Image();
            string getUrl = GetFileUploadedUrl();
            if (getUrl == null || getUrl == string.Empty)
                return;
            try
            {
                // Checking isValid
                imgUrlImage.Source = new BitmapImage(new Uri(getUrl, UriKind.Absolute));
            }
            catch
            {
                MessageBox.Show("This image is not valid", string.Empty, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            UrlImage = getUrl;
        }

        private string GetFileUploadedUrl(string filter = null)
        {
            OpenFileDialog openFile = new OpenFileDialog();

            if (filter != null)
            {
                openFile.Filter = filter;
            }

            bool? responsed = openFile.ShowDialog();
            if (responsed == null || responsed == false)
                return string.Empty;
            return openFile.FileName;
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
            string urlImage = UrlImage;
            Rated rated = cbRateds.SelectedItem as Rated;

            if (genres.Count == 0)
            {
                MessageBox.Show($"Please select the {"Movie genres"}", string.Empty, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                return false;
            }

            //if (Utilities.IsEmpty(urlImage))
            //{
            //    MessageBox.Show($"Please pick the {"movie image"}", string.Empty, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
            //    return false;
            //}
            if (rated == null)
            {
                MessageBox.Show($"Please pick the {"movie rated"}", string.Empty, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                return false;
            }
            return true;
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msbResult = CustomMessageBox.ShowOKCancel(
            "Do you confirm all changes",
            "Confirm infomation",
            "Confirm",
            "Cancel");
            if (msbResult == MessageBoxResult.OK)
            {
                if (!Checking())
                    return;

                List<Genre> genres = GetCheckedGenres();
                Rated rated = cbRateds.SelectedItem as Rated;
                string urlImage = UrlImage;

                int no = getMovieRepo().Length();
                MovieViewModel movieVM = new MovieViewModel();

                if (movie == null)
                    return;
                movie.Id = movieVM.GetId(no);
                movie.Name = txtName.Text;
                movie.Rated = rated;
                movie.Genres = genres;
                movie.RunningTime = Convert.ToInt32(txtRunningTime.Text);
                movie.ReleaseDate = Convert.ToDateTime(dateReleaseDate.SelectedDate);
                movie.Description = txtDescription.Text;
                movie.UrlImage = urlImage;


                frmReply = true;
                this.Close();
                return;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            frmReply = false;
            this.Close();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            ResetValues();
        }
    }
}
