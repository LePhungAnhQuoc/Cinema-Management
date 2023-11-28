using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for frmCustomer.xaml
    /// </summary>
    public partial class frmCustomer : Window
    {
        public Func<RepositoryBase<Customer>> getCustomerRepo { get; set; }

        public int no;
        public Customer newCustomer;
        public Func<ucBooking> getUcBooking;

        public frmCustomer()
        {
            InitializeComponent();
            txtCustomerName.Focus();
            newCustomer = new Customer();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            no = getCustomerRepo().Length();
        }

        private bool IsCheck()
        {
            if (Utilities.IsEmpty(txtCustomerName.Text))
            {
                MessageBox.Show("Please enter customer name", string.Empty, MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            if (Utilities.IsEmpty(txtPhone.Text))
            {
                MessageBox.Show("Please enter customer phone number", string.Empty, MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            return true;
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (!IsCheck())
            {
                return;
            }
            CustomerViewModel customerVM = new CustomerViewModel();
            customerVM.CustomerRepo = getCustomerRepo();
            newCustomer.Id = customerVM.GetId(no + 1);
            newCustomer.Name = txtCustomerName.Text;
            newCustomer.Phone = txtPhone.Text;

            if (getUcBooking == null)
                return;
            getUcBooking().getCustomer += () => newCustomer;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to cancel the booking?", null, MessageBoxButton.OKCancel, MessageBoxImage.Information, MessageBoxResult.Cancel);
            
            if (getUcBooking == null)
                return;
            getUcBooking().getCustomer = () => null;
            this.Close();
        }

        private bool CheckEnter(TextCompositionEventArgs e)
        {
            char enterKey = Convert.ToChar(13);
            if (e.Text == enterKey.ToString())
            {
                return true;
            }
            return false;
        }

        private void txtPhone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex reg = new Regex("^[0-9]*$");
            e.Handled = !reg.IsMatch(e.Text);
            if (CheckEnter(e))
                btnConfirm_Click(this, null);
        }

        private void txtCustomerName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (CheckEnter(e))
            {
                txtPhone.Focus();
            }
        }
    }
}
