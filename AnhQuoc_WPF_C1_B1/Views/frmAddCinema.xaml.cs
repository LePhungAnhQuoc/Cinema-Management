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
    /// Interaction logic for frmAddCinema.xaml
    /// </summary>
    public partial class frmAddCinema : Window
    {
        public Func<string> getFeature { get; set; }
        public Func<RepositoryBase<Cinema>> getCinemaRepo { get; set; }
        public ObservableCollection<Cinema> CinemaObs { get; set; }
        public Func<ucCinemaScheduleTable> getUcCinemaScheduleTable { get; set; }

        public Cinema GetCinema;
        public frmAddCinema()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CinemaObs = new ObservableCollection<Cinema>(getCinemaRepo().Gets());
            dgTable.ItemsSource = CinemaObs;

            if (getFeature() == "update")
            {
                lblTitle.Content = "Update Cinema Infomation";
            }
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GetCinema = (Cinema)dgTable.SelectedItem;
            }
            catch
            {
                Utilities.HandleError();
            }
            this.Close();
            if (getFeature() == "add")
                getUcCinemaScheduleTable().AddData(GetCinema);
            else if (getFeature() == "update")
                getUcCinemaScheduleTable().UpdateData(GetCinema);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
