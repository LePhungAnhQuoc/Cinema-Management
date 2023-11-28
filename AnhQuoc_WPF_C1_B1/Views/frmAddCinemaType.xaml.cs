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

namespace AnhQuoc_WPF_C1_B1
{
    /// <summary>
    /// Interaction logic for frmAddCinemaType.xaml
    /// </summary>
    public partial class frmAddCinemaType : Window
    {
        public Func<List<CinemaType>> getCinemaTypeRepo { get; set; }
        public ObservableCollection<CinemaType> CinemaTypeObs { get; set; }
        public Func<ucCinemaTypeScheduleTable> getUcCinemaTypeScheduleTable { get; set; }

        public CinemaType GetCinemaType;
        public frmAddCinemaType()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CinemaTypeObs = new ObservableCollection<CinemaType>(getCinemaTypeRepo());
            dgTable.ItemsSource = CinemaTypeObs;
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GetCinemaType = (CinemaType)dgTable.SelectedItem;
            }
            catch
            {
                Utilities.HandleError();
            }
            this.Close();
            getUcCinemaTypeScheduleTable().AddData(GetCinemaType);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
