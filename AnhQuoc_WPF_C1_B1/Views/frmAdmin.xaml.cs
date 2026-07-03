using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace AnhQuoc_WPF_C1_B1
{
    /// <summary>
    /// Interaction logic for frmAdmin.xaml
    /// </summary>
    public partial class frmAdmin : Window, INotifyPropertyChanged
    {
        #region Properties
        private ImageSource _AccountImage;

        public ImageSource AccountImage
        {
            get { return _AccountImage; }
            set 
            { 
                _AccountImage = value;
                OnPropertyChanged();
            }
        }


        public string Username { get; set; } = "Admin";
        private int _totalMovies;
        public int TotalMovies
        {
            get { return _totalMovies; }
            set
            {
                _totalMovies = value;
                OnPropertyChanged();
            }
        }
        private int _totalAccounts;
        public int TotalAccounts
        {
            get { return _totalAccounts; }
            set
            {
                _totalAccounts = value;
                OnPropertyChanged();
            }
        }

        private int _totalOrders;

        public int TotalOrders
        {
            get { return _totalOrders; }
            set 
            { 
                _totalOrders = value; 
                OnPropertyChanged(); 
            }
        }

        private int _totalOrderDetails;

        public int TotalOrderDetails
        {
            get { return _totalOrderDetails; }
            set
            {
                _totalOrderDetails = value;
                OnPropertyChanged();
            }
        }


        #endregion

        public Func<RepositoryBase<MovieSchedule>> getMovieScheduleRepo { get; set; }
        public Func<RepositoryBase<Cinema>> getCinemaRepo { get; set; }
        public Func<RepositoryBase<Movie>> getMovieRepo { get; set; }
        public Func<RepositoryBase<Genre>> getGenreRepo { get; set; }
        public Func<RepositoryBase<Rated>> getRatedRepo { get; set; }
        public Func<RepositoryBase<Order>> getOrderRepo { get; set; }
        public Func<RepositoryBase<OrderDetail>> getOrderDetailRepo { get; set; }

        public Func<RepositoryBase<Account>> getAccountRepo { get; set; }

        public Func<frmLogin> getFrmLogin { get; set; }
        public Func<Account> getAccount { get; set; }
        public Func<Movie> getDeleteMovie { get; set; }
        private ucMovieScheduleTable ucMovieScheduleTable;

        private Account _account;

        public Account Account
        {
            get { return _account; }
            set 
            { 
                _account = value;
                OnPropertyChanged();
            }
        }

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public frmAdmin()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Account = getAccount();
            AccountImage = Utilities.GetImageURL(Account.Image);
            this.DataContext = this;

            ucMovieScheduleTable = new ucMovieScheduleTable();
            ucMovieScheduleTable.getMovieScheduleRepo = getMovieScheduleRepo;
            ucMovieScheduleTable.getMovieRepo = getMovieRepo;
            ucMovieScheduleTable.getCinemaRepo = getCinemaRepo;

            string filePath = Environment.CurrentDirectory + "\\Data\\MovieSchedules";
            ucMovieScheduleTable.getFileSeat = () => filePath;
            ucMovieScheduleTable.getFrmAdmin = () => this;


            TotalMovies = getMovieRepo().Gets().Count;
            TotalAccounts = getAccountRepo().Gets().Count;
            TotalOrders = getOrderRepo().Gets().Count;
            TotalOrderDetails = getOrderDetailRepo().Gets().Count;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void tvStatistical_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ucStatistical ucStatistical = new ucStatistical();
            ucStatistical.getOrderRepo = getOrderRepo;

            stkView.Children.Clear();
            stkView.Children.Add(ucStatistical);
        }

        private void tvLogOut_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Hide();
            getFrmLogin().ClearLogin();
            getFrmLogin().Show();
            getFrmLogin().txtUsername.Focus();
        }
        
        private void tvMovie_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ucMovieTable ucMovieTable = new ucMovieTable();
            ucMovieTable.getFrmAdmin = () => this;

            ucMovieTable.getMovieRepo = getMovieRepo;
            ucMovieTable.getGenreRepo = getGenreRepo;
            ucMovieTable.getRatedRepo = getRatedRepo;
            ucMovieTable.getMovieScheduleRepo = getMovieScheduleRepo;

            stkView.Children.Clear();
            stkView.Children.Add(ucMovieTable);
        }

        private void tvMovieSchedule_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            stkView.Children.Clear();
            stkView.Children.Add(ucMovieScheduleTable);
        }

        public void DeleteMovieSchedule()
        {
            ucMovieScheduleTable.DeleteMovieSchedule(getDeleteMovie());
        }

        private void tvAccount_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ucUserTable ucUserTable = new ucUserTable();
            ucUserTable.getAccountRepo = getAccountRepo;
            stkView.Children.Clear();
            stkView.Children.Add(ucUserTable);
        }
    }
}
