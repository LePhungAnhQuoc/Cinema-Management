using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

namespace AnhQuoc_WPF_C1_B1.Views
{
    /// <summary>
    /// Interaction logic for frmLockUserNotify.xaml
    /// </summary>
    public partial class frmLockUserNotify : Window
    {
        public frmLockUserNotify(Account account)
        {
            InitializeComponent();
            this.DataContext = account;
        }

        private void BtnYes_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
