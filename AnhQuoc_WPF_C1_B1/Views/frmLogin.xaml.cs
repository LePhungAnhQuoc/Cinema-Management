using AnhQuoc_WPF_C1_B1.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AnhQuoc_WPF_C1_B1
{
    /// <summary>
    /// Interaction logic for frmLogin.xaml
    /// </summary>
    public partial class frmLogin : Window, INotifyPropertyChanged
    {
        #region Properties
        private Account _Account;

        public Account Account
        {
            get { return _Account; }
            set { 
                _Account = value;
                OnPropertyChanged();
            }
        }

        #endregion

        private AccountViewModel AccountVM;

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public frmLogin()
        {
            InitializeComponent();
            AccountVM = new AccountViewModel();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AccountVM.AccountRepo = App.UnitOfWork.GetRepositoryAccount;
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
            inputedAccount.PasswordHash = boxPassword.Password;
            inputedAccount.Username = inputedAccount.Username.Trim();
            inputedAccount.PasswordHash = inputedAccount.PasswordHash.Trim();

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
                Account = findedAccount;
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

        public void RoleNavigation()
        {
            switch (Account.Role)
            {
                case RoleTypes.Admin:
                    frmAdmin frmAdmin = new frmAdmin();
                    frmAdmin.getFrmLogin = () => this;
                    frmAdmin.getAccount = () => Account;
                    frmAdmin.getMovieScheduleRepo = () => App.UnitOfWork.GetRepositoryMovieSchedule;
                    frmAdmin.getMovieRepo = () => App.UnitOfWork.GetRepositoryMovie;
                    frmAdmin.getGenreRepo = () => App.UnitOfWork.GetRepositoryGenre;
                    frmAdmin.getRatedRepo = () => App.UnitOfWork.GetRepositoryRated;
                    frmAdmin.getOrderRepo = () => App.UnitOfWork.GetRepositoryOrder;
                    frmAdmin.getOrderDetailRepo = () => App.UnitOfWork.GetRepositoryOrderDetail;
                    frmAdmin.getCinemaRepo = () => App.UnitOfWork.GetRepositoryCinema;
                    frmAdmin.getAccountRepo = () => App.UnitOfWork.GetRepositoryAccount;
                    frmAdmin.Show();
                    break;
            }
            SessionManager.Instance.Login(Account);
        }
    }
}
