using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AnhQuoc_WPF_C1_B1
{
    /// <summary>
    /// Interaction logic for frmAdmin.xaml
    /// </summary>
    public partial class frmAdmin : Window
    {
        public Func<RepositoryBase<MovieSchedule>> getMovieScheduleRepo { get; set; }
        public Func<RepositoryBase<Cinema>> getCinemaRepo { get; set; }
        public Func<RepositoryBase<Movie>> getMovieRepo { get; set; }
        public Func<RepositoryBase<Genre>> getGenreRepo { get; set; }
        public Func<RepositoryBase<Rated>> getRatedRepo { get; set; }
        public Func<RepositoryBase<Order>> getOrderRepo { get; set; }
        public Func<RepositoryBase<Account>> getAccountRepo { get; set; }

        public Func<frmLogin> getFrmLogin { get; set; }
        public Func<Movie> getDeleteMovie { get; set; }
        private ucMovieScheduleTable ucMovieScheduleTable;
        public frmAdmin()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ucMovieScheduleTable = new ucMovieScheduleTable();
            ucMovieScheduleTable.getMovieScheduleRepo = getMovieScheduleRepo;
            ucMovieScheduleTable.getMovieRepo = getMovieRepo;
            ucMovieScheduleTable.getCinemaRepo = getCinemaRepo;

            string filePath = Environment.CurrentDirectory + "\\Data\\MovieSchedules";
            ucMovieScheduleTable.getFileSeat = () => filePath;
            ucMovieScheduleTable.getFrmAdmin = () => this;
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
            ucMovieScheduleTable.Btn_Delete(getDeleteMovie());
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
