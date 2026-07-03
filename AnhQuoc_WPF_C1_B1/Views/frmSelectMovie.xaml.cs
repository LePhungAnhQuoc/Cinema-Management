using System;
using System.Collections.Generic;
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
    /// Interaction logic for frmSelectMovie.xaml
    /// </summary>
    public partial class frmSelectMovie : Window, INotifyPropertyChanged
    {
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion


        public frmSelectMovie()
        {
            InitializeComponent();

            this.Loaded += FrmSelectMovie_Loaded;
            this.DataContext = this;
        }

        private void FrmSelectMovie_Loaded(object sender, RoutedEventArgs e)
        {
            ucMovieTable.getMovieRepo = () => App.UnitOfWork.GetRepositoryMovie;
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
