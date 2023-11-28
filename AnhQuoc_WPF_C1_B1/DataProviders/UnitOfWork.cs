using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_WPF_C1_B1
{
    class UnitOfWork
    {
        #region Fields
        private RepositoryBase<Account> Accounts;
        private RepositoryBase<Order> Orders;
        private RepositoryBase<Cinema> Cinemas;
        private RepositoryBase<Customer> Customers;
        private RepositoryBase<Rated> Rateds;
        private RepositoryBase<Movie> Movies;
        private RepositoryBase<Genre> Genres;
        private RepositoryBase<MovieSchedule> MovieSchedules;
        #endregion

        #region Repositories
        public RepositoryBase<Account> GetRepositoryAccount
        {
            get
            {
                if (this.Accounts == null)
                    this.Accounts = new RepositoryBase<Account>();
                return Accounts;
            }
        }

        public RepositoryBase<Cinema> GetRepositoryCinema
        {
            get
            {
                if (this.Cinemas == null)
                    this.Cinemas = new RepositoryBase<Cinema>();
                return Cinemas;
            }
        }

        public RepositoryBase<Customer> GetRepositoryCustomer
        {
            get
            {
                if (this.Customers == null)
                    this.Customers = new RepositoryBase<Customer>();
                return Customers;
            }
        }

        public RepositoryBase<Order> GetRepositoryOrder
        {
            get
            {
                if (this.Orders == null)
                    this.Orders = new RepositoryBase<Order>();
                return Orders;
            }
        }

        public RepositoryBase<Rated> GetRepositoryRated
        {
            get
            {
                if (this.Rateds == null)
                    this.Rateds = new RepositoryBase<Rated>();
                return Rateds;
            }
        }

        public RepositoryBase<Movie> GetRepositoryMovie
        {
            get
            {
                if (this.Movies == null)
                    this.Movies = new RepositoryBase<Movie>();
                return Movies;
            }
        }

        public RepositoryBase<Genre> GetRepositoryGenre
        {
            get
            {
                if (this.Genres == null)
                    this.Genres = new RepositoryBase<Genre>();
                return Genres;
            }
        }

        public RepositoryBase<MovieSchedule> GetRepositoryMovieSchedule
        {
            get
            {
                if (this.MovieSchedules == null)
                    this.MovieSchedules = new RepositoryBase<MovieSchedule>();
                return MovieSchedules;
            }
        }
        #endregion
        
        public UnitOfWork()
        {
            #region Allocates
            Accounts = new RepositoryBase<Account>();
            Cinemas = new RepositoryBase<Cinema>();
            Customers = new RepositoryBase<Customer>();
            Orders = new RepositoryBase<Order>();
            Rateds = new RepositoryBase<Rated>();
            Movies = new RepositoryBase<Movie>();
            Genres = new RepositoryBase<Genre>();
            MovieSchedules = new RepositoryBase<MovieSchedule>();
            #endregion
            
          
            SeedData seedData = new SeedData();

            Accounts.BulkInsert(seedData.LoadAccounts());
            Cinemas.BulkInsert(seedData.LoadCinemas());
            Customers.BulkInsert(seedData.LoadCustomers());
            Orders.BulkInsert(seedData.LoadOrders(Cinemas, Customers));
            Rateds.BulkInsert(seedData.LoadRateds());
            Genres.BulkInsert(seedData.LoadGenres());
            Movies.BulkInsert(seedData.LoadMovies(Genres.Items, Rateds.Items));
            MovieSchedules.BulkInsert(seedData.LoadMovieSchedules(Movies, Cinemas));
        }
    }
}
