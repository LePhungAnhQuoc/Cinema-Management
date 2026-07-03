using AnhQuoc_WPF_C1_B1.Helpers;
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
using Path = System.IO.Path;

namespace AnhQuoc_WPF_C1_B1
{
    /// <summary>
    /// Interaction logic for frmAddAccount.xaml
    /// </summary>
    public partial class frmAddAccount : Window, INotifyPropertyChanged
    {
        public Func<string> getFeature { get; set; }
        public Func<List<RoleTypes>> getRoles { get; set; }
        public Func<RepositoryBase<Account>> getAccountRepo { get; set; }
        public ObservableCollection<Account> AccountObs { get; set; }
        public Func<ucUserTable> getUcUserTable { get; set; }


        #region Properties
        private ImageSource _ImageURL;
        public ImageSource ImageURL
        {
            get { return _ImageURL; }
            set
            {
                _ImageURL = value;
                OnPropertyChanged();
            }
        }


        private Account _GetAccount;

        public Account GetAccount
        {
            get { return _GetAccount; }
            set 
            { 
                _GetAccount = value;
                OnPropertyChanged();
            }
        }

        public string Feature { get; set; }
        #endregion


        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        public frmAddAccount()
        {
            InitializeComponent();

            // Set the window's startup position to the top of the screen
            this.Top = 0;

            // Set the height to match the current screen's working area (excluding taskbar)
            this.Height = SystemParameters.WorkArea.Height;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Feature = getFeature();

            if (getFeature() == "update")
            {
                GetAccount = getUcUserTable().CurrentItem;
            }
            else
            {
                GetAccount = new Account();
                GetAccount.Role = RoleTypes.Cashier;
            }
            this.DataContext = this;

            AccountObs = new ObservableCollection<Account>(getAccountRepo().Gets());
            cbRole.ItemsSource = new ObservableCollection<RoleTypes>(getRoles());

            if (getFeature() == "update")
            {
                txtUsername.IsEnabled = false;
            }
            if (GetAccount.Image != string.Empty)
            {
                ImageURL = Utilities.GetImageURL(GetAccount.Image);
            }
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            if (getFeature() == "add")
            {
                GetAccount.Password = Argon2idHasher.HashPassword(GetAccount.Password);
                getUcUserTable().AddData(GetAccount);
            }
            else if (getFeature() == "update")
                getUcUserTable().UpdateData(GetAccount);
        }

        private void btnReplaceImage_Click(object sender, RoutedEventArgs e)
        {
            string imageFile = null;
            Utilities.SelectImage("UserImages", ref imageFile);
            GetAccount.Image = imageFile;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete this user", "Warning", MessageBoxButton.OKCancel);
            if (messageBoxResult == MessageBoxResult.OK)
            {
                getAccountRepo().Remove(GetAccount);
                
                // Save to database
                AccountViewModel accountViewModel = new AccountViewModel();
                accountViewModel.AccountRepo = getAccountRepo();
                accountViewModel.WriteRemoveData(GetAccount);

                this.Close();
            }
        }
    }
}
