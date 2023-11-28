using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AnhQuoc_WPF_C1_B1
{
    /// <summary>
    /// Interaction logic for ucUserTable.xaml
    /// </summary>
    public partial class ucUserTable : UserControl, INotifyPropertyChanged
    {
        public Func<RepositoryBase<Account>> getAccountRepo { get; set; }

        private ObservableCollection<Account> getSource;
        private AccountViewModel accountVM;

        public Account CurrentItem;
        private Button btnLock;
        private Button btnUnlock;

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        public ucUserTable()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            accountVM = new AccountViewModel();
            accountVM.AccountRepo = getAccountRepo();
            getSource = new ObservableCollection<Account>(getAccountRepo().Gets());

            getSource.RemoveAt(0);
            dgTable.ItemsSource = getSource;
        }

        public void AddData(Account newItem)
        {
            accountVM.AccountRepo.Items.Add(newItem);
            getSource.Add(newItem);

            accountVM.WriteData(newItem);
        }

        public void UpdateData(Account data)
        {
            Account oldData = CurrentItem;
            CurrentItem.Password = data.Password;

            getSource.Insert(getSource.IndexOf(oldData), data);
            getSource.Remove(oldData);

            accountVM.WriteUpdateData(data);
        }

        private void LoadUcCinemaPicker(string feature)
        {
            AccountViewModel accountVM = new AccountViewModel();
            accountVM.AccountRepo = getAccountRepo();

            frmAddAccount frmAddAccount = new frmAddAccount();
            frmAddAccount.getAccountRepo = getAccountRepo;
            EnumViewModel enumVM = new EnumViewModel();
            List<RoleTypes> roles = enumVM.GetValues<RoleTypes>().ToList();
            roles.RemoveAt(0);
            frmAddAccount.getRoles = () => roles;
            frmAddAccount.getUcUserTable = () => this;
            
            if (feature == "update")
            {
                CurrentItem = dgTable.SelectedValue as Account;
            }
            frmAddAccount.getFeature = () => feature;
            frmAddAccount.ShowDialog();
        }
        

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            LoadUcCinemaPicker("add");
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msbResult = MessageBox.Show("Do you want to remove this item", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning, MessageBoxResult.Cancel);
            if (msbResult == MessageBoxResult.Cancel)
                return;
            Account newItem = null;
            try
            {
                newItem = (Account)dgTable.SelectedItem;
            }
            catch
            {
                Utilities.HandleError();
            }
            getSource.Remove(newItem);
            accountVM.AccountRepo.Remove(newItem);

            accountVM.WriteRemoveData(newItem);
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            LoadUcCinemaPicker("update");
        }

        private void btnLock_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msbResult = MessageBox.Show("Do you want to lock this item", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning, MessageBoxResult.Cancel);
            if (msbResult == MessageBoxResult.Cancel)
                return;
            Account newItem = null;
            try
            {
                newItem = (Account)dgTable.SelectedItem;
            }
            catch
            {
                Utilities.HandleError();
            }
            newItem.Status = 0;
            accountVM.WriteUpdateData(newItem);

            btnLock = sender as Button;
            btnLock.IsEnabled = false;
            if (btnUnlock != null)
                btnUnlock.IsEnabled = true;
        }

        private void btnUnlock_Click(object sender, RoutedEventArgs e)
        {
         
        }

        private void btnLockState_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            MessageBoxResult msbResult = MessageBox.Show($"Do you want to {btn.Content} this item", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning, MessageBoxResult.Cancel);
            if (msbResult == MessageBoxResult.Cancel)
                return;
            Account newItem = null;
            try
            {
                newItem = (Account)dgTable.SelectedItem;
            }
            catch
            {
                Utilities.HandleError();
            }
            bool state = Convert.ToBoolean(newItem.Status);
            newItem.Status = Convert.ToInt32(!state);
            accountVM.WriteUpdateData(newItem);
        }
    }
}
