using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace AnhQuoc_WPF_C1_B1
{
    /// <summary>
    /// Interaction logic for frmLogin.xaml
    /// </summary>
    public partial class frmLogin : Window
    {
        public Func<RepositoryBase<MovieSchedule>> getMovieScheduleRepo { get; set; }
        public Func<RepositoryBase<Movie>> getMovieRepo { get; set; }
        public Func<RepositoryBase<Genre>> getGenreRepo { get; set; }
        public Func<RepositoryBase<Rated>> getRatedRepo { get; set; }
        public Func<RepositoryBase<Order>> getOrderRepo { get; set; }
        public Func<RepositoryBase<Account>> getAccountRepo { get; set; }
        public Func<RepositoryBase<Cinema>> getCinemaRepo { get; set; }
        public Func<RepositoryBase<Customer>> getCustomerRepo { get; set; }

        private AccountViewModel AccountVM;
        public frmLogin()
        {
            InitializeComponent();
            AccountVM = new AccountViewModel();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AccountVM.AccountRepo = getAccountRepo();
            txtUsername.Focus();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        
        public void ClearLogin()
        {
            txtUsername.Clear();
            boxPassword.Clear();
            txtUsername.Focus();
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            Account inputedAccount = new Account();
            inputedAccount.Username = txtUsername.Text;
            inputedAccount.Password = boxPassword.Password;

            Account findedAccount = AccountVM.Find(inputedAccount, 1);

            // Checking account
            if (findedAccount == null)
            {
                MessageBox.Show("Incorrect username or password", string.Empty, MessageBoxButton.OK, MessageBoxImage.Error);
                ClearLogin();
            }
            else
            {
                this.Hide();
                frmAdmin frmAdmin = new frmAdmin();
                frmAdmin.getFrmLogin = () => this;
                frmCashier frmCashier = new frmCashier();
                frmCashier.getFrmLogin = () => this;
                switch (findedAccount.Role)
                {
                    case RoleTypes.Admin:
                        frmAdmin.getMovieScheduleRepo = getMovieScheduleRepo;
                        frmAdmin.getMovieRepo = getMovieRepo;
                        frmAdmin.getGenreRepo = getGenreRepo;
                        frmAdmin.getRatedRepo = getRatedRepo;
                        frmAdmin.getOrderRepo = getOrderRepo;
                        frmAdmin.getCinemaRepo = getCinemaRepo;
                        frmAdmin.getAccountRepo = getAccountRepo;
                        frmAdmin.Show();
                        break;
                    case RoleTypes.Cashier:
                        frmCashier.getMovieRepo = getMovieRepo;
                        frmCashier.getOrderRepo = getOrderRepo;
                        frmCashier.getCinemaRepo = getCinemaRepo;
                        frmCashier.getCustomerRepo = getCustomerRepo;
                        frmCashier.getMovieScheduleRepo = getMovieScheduleRepo;
                        frmCashier.Show();
                        break;
                }
            }
        }

        private bool IsCheckEnter(TextCompositionEventArgs e)
        {
            char enterKey = Convert.ToChar(13);
            if (e.Text == enterKey.ToString())
            {
                return true;
            }
            return false;
        }

        private void boxPassword_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (IsCheckEnter(e))
            {
                btnSignIn_Click(this, null);
            }
        }

        private void txtUsername_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (IsCheckEnter(e))
            {
                boxPassword.Focus();
            }
        }
    }
}
