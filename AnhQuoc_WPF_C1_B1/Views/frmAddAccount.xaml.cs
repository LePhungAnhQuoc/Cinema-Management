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
using System.Windows.Shapes;

namespace AnhQuoc_WPF_C1_B1
{
    /// <summary>
    /// Interaction logic for frmAddAccount.xaml
    /// </summary>
    public partial class frmAddAccount : Window
    {
        public Func<string> getFeature { get; set; }
        public Func<List<RoleTypes>> getRoles { get; set; }
        public Func<RepositoryBase<Account>> getAccountRepo { get; set; }
        public ObservableCollection<Account> AccountObs { get; set; }
        public Func<ucUserTable> getUcUserTable { get; set; }

        public Account GetAccount;
        public frmAddAccount()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AccountObs = new ObservableCollection<Account>(getAccountRepo().Gets());
            cbRole.ItemsSource = new ObservableCollection<RoleTypes>(getRoles());

            if (getFeature() == "update")
            {
                lblTitle.Content = "Update Account Infomation";
                txtUsername.Text = getUcUserTable().CurrentItem.Username;
                txtPassword.Text = getUcUserTable().CurrentItem.Password;
                cbRole.SelectedItem = getUcUserTable().CurrentItem.Role;
                txtUsername.IsEnabled = false;
            }
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GetAccount = new Account();
                GetAccount.Username = txtUsername.Text;
                GetAccount.Password = txtPassword.Text;
                GetAccount.Role = RoleTypes.Cashier;
                GetAccount.Status = 1;

                AccountViewModel accountVM = new AccountViewModel();
                accountVM.AccountRepo  = getAccountRepo();
                int accStatus = 1;
                Account accCheck = accountVM.Find(GetAccount, accStatus);

                if (accCheck != null)
                {
                    MessageBox.Show(Utilities.GetIsExistMessage(true, "Account"));
                    return;
                }
            }
            catch
            {
                Utilities.HandleError();
            }
            this.Close();
            if (getFeature() == "add")
                getUcUserTable().AddData(GetAccount);
            else if (getFeature() == "update")
                getUcUserTable().UpdateData(GetAccount);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
