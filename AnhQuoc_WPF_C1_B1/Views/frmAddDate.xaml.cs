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
    /// Interaction logic for frmAddDate.xaml
    /// </summary>
    public partial class frmAddDate : Window
    {
        public Func<string> getFeature { get; set; }
        public Func<ucDateScheduleTable> getUcDateScheduleTable { get; set; }
        public frmAddDate()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (getFeature() == "update")
            {
                lblTitle.Content = "Update Date Schedule Infomation";

                datePicker.SelectedDate = getUcDateScheduleTable().CurrentItem.Date;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            DateTime data = DateTime.Now;
            try { data = (DateTime)datePicker.SelectedDate; }
            catch
            {
                MessageBox.Show("Please pick a date for schedule");
                return;
            }
            this.Close();
            if (getFeature() == "add")
                getUcDateScheduleTable().AddData(data);
            else if (getFeature() == "update")
                getUcDateScheduleTable().UpdateData(data);
        }
    }
}
