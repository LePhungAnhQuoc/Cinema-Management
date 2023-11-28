using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace AnhQuoc_WPF_C1_B1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UnitOfWork unitOfWork;

        private AccountViewModel AccountVM;
        private CinemaViewModel CinemaVM;
        private OrderViewModel OrderVM;
        private CustomerViewModel CustomerVM;
        private RatedViewModel RatedVM;
        private GenreViewModel GenreVM;
        private MovieViewModel MovieVM;
        private MovieScheduleViewModel MovieScheduleVM;

        public MainWindow()
        {
            unitOfWork = new UnitOfWork();
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            AccountVM = new AccountViewModel();
            AccountVM.getList(unitOfWork.GetRepositoryAccount);
            CustomerVM = new CustomerViewModel();
            CustomerVM.getList(unitOfWork.GetRepositoryCustomer);
            CinemaVM = new CinemaViewModel();
            CinemaVM.getList(unitOfWork.GetRepositoryCinema);
            OrderVM = new OrderViewModel();
            OrderVM.getList(unitOfWork.GetRepositoryOrder);
            RatedVM = new RatedViewModel();
            RatedVM.getList(unitOfWork.GetRepositoryRated);
            GenreVM = new GenreViewModel();
            GenreVM.getList(unitOfWork.GetRepositoryGenre);
            MovieVM = new MovieViewModel();
            MovieVM.getList(unitOfWork.GetRepositoryMovie);

            MovieScheduleVM = new MovieScheduleViewModel();
            MovieScheduleVM.getList(unitOfWork.GetRepositoryMovieSchedule);
            this.Hide();

            frmLogin frmLogin = new frmLogin();

            frmLogin.getMovieScheduleRepo = () => MovieScheduleVM.MovieScheduleRepo;
            frmLogin.getMovieRepo = () => MovieVM.MovieRepo;
            frmLogin.getGenreRepo = () => GenreVM.GenreRepo;
            frmLogin.getRatedRepo = () => RatedVM.RatedRepo;
            frmLogin.getOrderRepo = () => OrderVM.OrderRepo;
            frmLogin.getCinemaRepo = () => CinemaVM.CinemaRepo;
            frmLogin.getAccountRepo = () => AccountVM.AccountRepo;
            frmLogin.getCustomerRepo = () => CustomerVM.CustomerRepo;

            frmLogin.Show();
        }
        
        private void Window_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }


        public static void removeGridCol(Grid grid1, int index)
        {
            if (grid1.ColumnDefinitions.Count <= 0)
            {
                return;
            }
            else
            {
                try { grid1.ColumnDefinitions.RemoveAt(index); }
                catch { }
            }
        }

        public static void addGridCol(Grid grid1)
        {
            var colDef1 = new ColumnDefinition();
            grid1.ColumnDefinitions.Add(colDef1);
        }

        public static void removeGridRow(Grid grid1, int index)
        {
            if (grid1.RowDefinitions.Count <= 0)
            {
                return;
            }
            else
            {
                grid1.RowDefinitions.RemoveAt(index);
            }
        }

        public static void addRow(Grid grid1)
        {
            var rowDef1 = new RowDefinition();
            grid1.RowDefinitions.Add(rowDef1);
        }

        public static string FeatureNotDevelop()
        {
            return "This feature is not develop";
        }
    }
}
