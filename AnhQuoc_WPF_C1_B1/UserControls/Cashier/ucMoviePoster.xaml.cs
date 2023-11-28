using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
    /// Interaction logic for ucMoviePoster.xaml
    /// </summary>
    public partial class ucMoviePoster : UserControl, INotifyPropertyChanged
    {
        public Func<Movie> getMovie { get; set; }

        private Movie _movie;
        public Movie movie
        {
            get { return _movie; }
            set
            {
                _movie = value;
                OnPropertyChanged("movie");
            }
        }

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        // Phương thức hỗ trợ emplement
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        private string _UrlImage;
        public string UrlImage
        {
            get { return _UrlImage; }
            set
            {
                _UrlImage = value;
                OnPropertyChanged("UrlImage");
            }
        }

        public ucMoviePoster()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            movie = getMovie();
            UrlImage = movie.UrlImage;
            if (UrlImage == string.Empty)
            {
                UrlImage = Environment.CurrentDirectory + "\\" + Constants.fNoImage;
            }
        }

        private void btnChoose_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(MainWindow.FeatureNotDevelop(), null);
        }

        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(MainWindow.FeatureNotDevelop(), null);
        }
    }
}
