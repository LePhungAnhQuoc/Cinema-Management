using AnhQuoc_WPF_C1_B1.UserControls.Admin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace AnhQuoc_WPF_C1_B1
{
    /// <summary>
    /// Interaction logic for frmCashier.xaml
    /// </summary>
    public partial class frmCashier : Window, INotifyPropertyChanged
    {
        #region Fields
        private bool _isMenuOpen = false;
        private frmLogin frmLogin;

        // Temporary backup
        private List<UIElement> _cachedChildren = new List<UIElement>();
        #endregion


        public Func<Account> getAccount { get; set; }
        public Func<RepositoryBase<Order>> getOrderRepo { get; set; }
        public Func<RepositoryBase<Movie>> getMovieRepo { get; set; }
        public Func<RepositoryBase<MovieSchedule>> getMovieScheduleRepo { get; set; }
        public Func<RepositoryBase<Cinema>> getCinemaRepo { get; set; }
        public Func<RepositoryBase<Customer>> getCustomerRepo { get; set; }
        public Func<MainWindow> GetMainWindowForm { get; set; }


        #region Properties
        private Account _Account;
        public Account Account
        {
            get { return _Account; }
            set
            {
                _Account = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Movie> _Movies;

        public ObservableCollection<Movie> Movies
        {
            get { return _Movies; }
            set 
            { 
                _Movies = value;
                OnPropertyChanged();
            }
        }



        public bool IsUserLoggedIn
        {
            get
            {
                return SessionManager.Instance.IsLoggedIn;
            }
        }

        #endregion

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region ViewModels
        private MovieViewModel movieViewModel;
        #endregion

        public frmCashier()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            this.Loaded += FrmCashier_Loaded;
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_isMenuOpen)
            {
                Storyboard openStoryboard = (Storyboard)this.FindResource("OpenMenuStoryboard");
                openStoryboard.Begin(this);
            }
            else
            {
                Storyboard closeStoryboard = (Storyboard)this.FindResource("CloseMenuStoryboard");
                closeStoryboard.Begin(this);
            }

            _isMenuOpen = !_isMenuOpen;
        }
       
        private void FrmCashier_Loaded(object sender, RoutedEventArgs e)
        {
            movieViewModel = new MovieViewModel();
            movieViewModel.MovieRepo = getMovieRepo();

            Movies = new ObservableCollection<Movie>(getMovieRepo().Items);
            this.DataContext = this;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void tvBooking_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            frmLogin = new frmLogin();
            frmLogin.ShowDialog();

            frmLogin.RoleNavigation();
            OnPropertyChanged(nameof(IsUserLoggedIn));
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.LogOut(this, frmLogin);

            frmLogin.RoleNavigation();
            OnPropertyChanged(nameof(IsUserLoggedIn));
        }

        private void txtSearchMovie_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Don't let the keypress bubble up further
                e.Handled = true;

                btnSearchMovie_Click(btnSearchMovie, new RoutedEventArgs());
            }
        }

        private void btnSearchMovie_Click(object sender, RoutedEventArgs e)
        {
            var listMovieFinded = movieViewModel.SearchMovies(txtSearchMovie.Text);

            Movies = new ObservableCollection<Movie>(listMovieFinded);
        }

        public void MoviePoster_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Border border = sender as Border;
            if (border == null)
                return;

            string movieName = border.Tag.ToString();

            Movie movie = movieViewModel.FindByName(movieName);
            if (movie == null)
            {
                Utilities.HandleError();
                return;
            }

            ucMovieInformation ucMovieInformation = new ucMovieInformation();
            ucMovieInformation.GetMovie = movie;
            ucMovieInformation.GetRole = RoleTypes.Cashier;
            ucMovieInformation.IsReadOnly = true;

            Window window = new Window();
            window.Content = ucMovieInformation;
            window.ShowDialog();
        }
    }
}
