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
    /// Interaction logic for frmAddTime.xaml
    /// </summary>
    public partial class frmAddTime : Window
    {
        public Func<double> getMovieRunningTimes { get; set; }
        public Func<string> getFeature { get; set; }
        public Func<ucTimeScheduleTable> getUcTimeScheduleTable { get; set; }
        public frmAddTime()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (getFeature() == "update")
            {
                lblTitle.Content = "Update Time Schedule Infomation";
                try
                {
                    sfTimePicker.Value = Convert.ToDateTime(getUcTimeScheduleTable().currentItem.Time.ToString());
                }
                catch { }
            }
        }
        
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan data = TimeSpan.Zero;
            try
            {
                DateTime tempData = DateTime.Parse(sfTimePicker.Value.ToString());
                data = tempData.TimeOfDay;
            }
            catch
            {
                MessageBox.Show("Please pick a Time for schedule");
                return;
            }
            this.Close();
            if (getFeature() == "add")
                getUcTimeScheduleTable().AddData(data);
            else if (getFeature() == "update")
                getUcTimeScheduleTable().UpdateData(data);
        }
    }
}
