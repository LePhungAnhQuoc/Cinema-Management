using System;
using System.Collections.Generic;
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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AnhQuoc_WPF_C1_B1
{
    /// <summary>
    /// Interaction logic for ucMoviePickerTable.xaml
    /// </summary>
    public partial class ucMoviePickerTable : UserControl
    {
        public Func<string> getFeature { get; set; }
        public Func<MovieSchedule> getCurrentMovieSchedule { get; set; }

        public Func<RepositoryBase<Movie>> getMovieRepo { get; set; }
        public ObservableCollection<Movie> movieObs { get; set; }
        public Func<ucMovieScheduleTable> getUcMovieScheduleTable { get; set; }

        public Movie GetMovie;
        public ucMoviePickerTable()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            movieObs = new ObservableCollection<Movie>(getMovieRepo().Gets());
            dgTable.ItemsSource = movieObs;
        }

        private void dgTable_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            GetMovie = dgTable.SelectedItem as Movie;
            if (getFeature() == "add")
                getUcMovieScheduleTable().AddData(GetMovie);
            else if (getFeature() == "update")
                getUcMovieScheduleTable().UpdateData(getCurrentMovieSchedule(), GetMovie);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            getUcMovieScheduleTable().Content = getUcMovieScheduleTable().thisContent;
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            dgTable_MouseDoubleClick(null, null);
        }
    }
}
