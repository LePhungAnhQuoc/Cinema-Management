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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AnhQuoc_WPF_C1_B1
{
    /// <summary>
    /// Interaction logic for ucDisplayListPoster.xaml
    /// </summary>
    public partial class ucDisplayListPoster : UserControl
    {
        public Func<RepositoryBase<Movie>> getMovieRepo { get; set; }
        public Func<ucBooking> getUcBooking { get; set; }
        public Func<ucScheduleBooking> getUcScheduleBooking { get; set; }

        public ucDisplayListPoster()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (getMovieRepo == null)
            {
                MessageBox.Show("Error unsolve");
                return;
            }
            foreach (Movie movie in getMovieRepo().Gets())
            {
                ListBoxItem lbItem = new ListBoxItem();
                ucMoviePoster ucMoviePoster = new ucMoviePoster();
                ucMoviePoster.getMovie = () => movie;
                lbItem.Content = ucMoviePoster;
                lbMovies.Items.Add(lbItem);
            }
        }

        private void lbMovies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxItem lbSelected = lbMovies.SelectedItem as ListBoxItem;
            if (lbSelected == null) // Lỗi Ép kiểu
                return;
            ucMoviePoster item = lbSelected.Content as ucMoviePoster;
            if (item == null) // Lỗi Ép kiểu
                return;
            getUcBooking().getMovie = () => item.movie;
        }
    }
}
