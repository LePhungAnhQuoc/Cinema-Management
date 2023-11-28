using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Xml;

namespace AnhQuoc_WPF_C1_B1
{
    /// <summary>
    /// Interaction logic for ucBooking.xaml
    /// </summary>
    public partial class ucBooking : UserControl
    {
        public Func<Customer> getCustomer;
        public Func<RepositoryBase<Order>> getOrderRepo { get; set; }
        public Func<RepositoryBase<MovieSchedule>> getMovieScheduleRepo { get; set; }
        public Func<RepositoryBase<Cinema>> getCinemaRepo { get; set; }
        public Func<RepositoryBase<Customer>> getCustomerRepo { get; set; }
        public Func<List<List<Seat>>> getSeatsRepo { get; set; }

        public Order newOrder;

        private Func<Movie> _getMovie;
        public Func<Movie> getMovie
        {
            get { return _getMovie; }
            set
            {
                _getMovie = value;
                OnGetMovieChange();
            }
        }

        public ucBooking()
        {
            InitializeComponent();
            newOrder = new Order();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            int no = getOrderRepo().Length();
            OrderViewModel orderVM = new OrderViewModel();
            newOrder.Id = orderVM.GetId(no + 1);

            ucDisplayListPoster ucDisplayPosters = new ucDisplayListPoster();

            MovieScheduleViewModel movieScheduleVM = new MovieScheduleViewModel();
            movieScheduleVM.MovieScheduleRepo = getMovieScheduleRepo();

            List<Movie> movies = movieScheduleVM.FillMovie();
            if (!IsCheckSchedule(movies))
            {
                MessageBox.Show(Utilities.GetListEmptyMessage("Movie", "yet"));
                return;
            }
            RepositoryBase<Movie> getMovieRepo = new RepositoryBase<Movie>();
            getMovieRepo.Items = movies;

            ucDisplayPosters.getMovieRepo = () => getMovieRepo;
            ucDisplayPosters.getUcBooking = () => this;

            this.Content = ucDisplayPosters;
        }

        public bool IsCheckSchedule(List<Movie> movies)
        {
            if (movies.Count == 0)
                return false;
            return true;
        }

        public Customer LoadFrmCustomer()
        {
            frmCustomer frmCustomer = new frmCustomer();
            frmCustomer.getCustomerRepo = getCustomerRepo;
            frmCustomer.getUcBooking = () => this;
            frmCustomer.ShowDialog();

            return getCustomer();
        }

        public void LoadUcSeatBooking()
        {
            ucSeatBooking ucSeatBooking = new ucSeatBooking();
            ucSeatBooking.getOrderRepo = getOrderRepo;
            ucSeatBooking.getCustomerRepo = getCustomerRepo;

            ucSeatBooking.getSeats = getSeatsRepo;
            ucSeatBooking.getUcBooking = () => this;

            this.Content = ucSeatBooking;
        }

        private void OnGetMovieChange()
        {
            if (getMovie == null)
                return;
            newOrder.MovieOrder.Movie = getMovie();
            MovieScheduleViewModel movieScheduleVM = new MovieScheduleViewModel();
            movieScheduleVM.MovieScheduleRepo = getMovieScheduleRepo();

            MovieSchedule getMovieSchedule = movieScheduleVM.GetByMovie(getMovie());
            ucScheduleBooking ucScheduleBooking = new ucScheduleBooking();
            ucScheduleBooking.getMovieSchedule = () => getMovieSchedule;
            ucScheduleBooking.getUcBooking = () => this;

            this.Content = ucScheduleBooking;
        }
    }
}
