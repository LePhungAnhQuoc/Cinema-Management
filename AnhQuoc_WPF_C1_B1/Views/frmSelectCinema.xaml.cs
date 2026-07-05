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

namespace AnhQuoc_WPF_C1_B1.Views
{
    /// <summary>
    /// Interaction logic for frmSelectCinema.xaml
    /// </summary>
    public partial class frmSelectCinema : Window, INotifyPropertyChanged
    {
        public Func<List<Cinema>> GetCinemaSource { get; set; }


        public ObservableCollection<Cinema> Cinemas { get; set; }

        private IList<Cinema> _SelectedItems;
        public IList<Cinema> SelectedItems
        {
            get { return _SelectedItems; }
            set
            {
                _SelectedItems = value;
                OnPropertyChanged();
            }
        }

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        public frmSelectCinema()
        {
            InitializeComponent();
            this.Loaded += FrmSelectCinema_Loaded;
        }

        private void FrmSelectCinema_Loaded(object sender, RoutedEventArgs e)
        {
            RepositoryBase<Cinema> repo = new RepositoryBase<Cinema>(GetCinemaSource());
            ucCinemaScheduleTable.getCinemaRepo = () => repo;
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            SelectedItems = ucCinemaScheduleTable.SelectedItems;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
