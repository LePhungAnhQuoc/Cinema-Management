using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace AnhQuoc_WPF_C1_B1
{
    class SeedData
    {
        // ----------------- LOAD DATA  ----------------- //

        public List<Account> LoadAccounts()
        {
            List<Account> lstAccount = new List<Account>();
            DataProvider.Instance.pathData = Constants.fAccounts;
            DataProvider.Instance.Open();
            XmlNodeList lstNode = DataProvider.Instance.nodeRoot.ChildNodes;

            foreach (XmlNode node in lstNode)
            {
                Account newAccount = null;
                try
                {
                    newAccount = new Account();

                    newAccount.Username = node.Attributes["Username"].Value;
                    newAccount.Password = node.Attributes["Password"].Value;
                    newAccount.Role = (RoleTypes)Enum.Parse(typeof(RoleTypes), node.Attributes["Role"].Value);
                    newAccount.Status = Convert.ToInt32(node.Attributes["Status"].Value);

                }
                catch
                {
                    Utilities.HandleError();
                }
                lstAccount.Add(newAccount);
            }
            DataProvider.Instance.Close();
            return lstAccount;
        }

        public List<Cinema> LoadCinemas()
        {
            List<Cinema> cinemas = new List<Cinema>();
            DataProvider.Instance.pathData = Constants.fCinemas;
            DataProvider.Instance.Open();
            XmlNodeList lstNode = DataProvider.Instance.nodeRoot.ChildNodes;

            foreach (XmlNode cinemaNode in lstNode)
            {
                Cinema newCinema = new Cinema
                {
                    Id = cinemaNode.Attributes["Id"].Value,
                    Name = cinemaNode.Attributes["Name"].Value,
                };
                try
                {
                    newCinema.PriceCenter = Convert.ToDouble(cinemaNode.Attributes["PriceCenter"].Value);
                    newCinema.DecreasePrice = Convert.ToDouble(cinemaNode.Attributes["DecreasePrice"].Value);
                    newCinema.Type = (CinemaType)Enum.Parse(typeof(CinemaType), cinemaNode.Attributes["Type"].Value);
                    string[] strSize = cinemaNode.Attributes["Size"].Value.Split(' ');
                    newCinema.Size.Width = Convert.ToInt32(strSize[0]);
                    newCinema.Size.Height = Convert.ToInt32(strSize[1]);
                }
                catch
                {
                    Utilities.HandleError();
                }
                cinemas.Add(newCinema);
            }
            DataProvider.Instance.Close();
            LoadSeatsCinemas(cinemas);
            return cinemas;
        }

        public List<Genre> LoadGenres()
        {
            List<Genre> lstGenre = new List<Genre>();
            DataProvider.Instance.pathData = Constants.fGenres;
            DataProvider.Instance.Open();
            XmlNodeList lstNode = DataProvider.Instance.nodeRoot.ChildNodes;

            foreach (XmlNode node in lstNode)
            {
                Genre newGenre = new Genre
                {
                    Id = node.Attributes["Id"].Value,
                    Name = node.Attributes["Name"].Value
                };
                lstGenre.Add(newGenre);
            }
            DataProvider.Instance.Close();
            return lstGenre;
        }

        public List<Rated> LoadRateds()
        {
            List<Rated> lstRated = new List<Rated>();
            DataProvider.Instance.pathData = Constants.fRateds;
            DataProvider.Instance.Open();
            XmlNodeList lstNode = DataProvider.Instance.nodeRoot.ChildNodes;

            foreach (XmlNode node in lstNode)
            {
                Rated newRated = new Rated
                {
                    Id = node.Attributes["Id"].Value,
                    Detail = node.Attributes["Name"].Value
                };
                lstRated.Add(newRated);
            }
            DataProvider.Instance.Close();
            return lstRated;
        }

        public List<Movie> LoadMovies(List<Genre> lstGenre, List<Rated> lstRated)
        {
            GenreViewModel genreVM = new GenreViewModel();
            genreVM.GenreRepo = new RepositoryBase<Genre>();
            genreVM.GenreRepo.Items = lstGenre;

            RatedViewModel ratedVM = new RatedViewModel();
            ratedVM.RatedRepo = new RepositoryBase<Rated>();
            ratedVM.RatedRepo.Items = lstRated;

            List<Movie> lstMovie = new List<Movie>();
            DataProvider.Instance.pathData = Constants.fMovies;
            DataProvider.Instance.Open();
            XmlNodeList lstNode = DataProvider.Instance.nodeRoot.ChildNodes;

            foreach (XmlNode node in lstNode)
            {
                Movie newMovie = new Movie();
                newMovie.Id = node.Attributes["Id"].Value;
                newMovie.Name = node.Attributes["Name"].Value;

                string[] genresStr = node.Attributes["Genres"].Value.Split(' ');
                foreach (string genreId in genresStr)
                {
                    Genre genreFinded = genreVM.FindById(genreId);
                    if (genreFinded == null)
                        return null;
                    newMovie.Genres.Add(genreFinded);
                }
                Rated ratedFinded = ratedVM.FindById(node.Attributes["Rated"].Value);
                if (ratedFinded == null)
                    return null;
                newMovie.Rated = ratedFinded;

                try
                {
                    newMovie.RunningTime = Convert.ToDouble(node.Attributes["RunningTime"].Value);
                    newMovie.ReleaseDate = Convert.ToDateTime(node.Attributes["ReleaseDate"].Value);
                }
                catch { }
                lstMovie.Add(newMovie);
            }
            DataProvider.Instance.Close();
            LoadMovieDetail(lstMovie);
            return lstMovie;
        }

        public void LoadMovieDetail(List<Movie> movies)
        {
            DataProvider.Instance.pathData = Constants.fMovieDetails;
            DataProvider.Instance.Open();
            XmlNode parentNode = DataProvider.Instance.nodeRoot;
            foreach (Movie newMovie in movies)
            {
                XmlNode node = parentNode.SelectSingleNode($"MovieDetail[@Id='{newMovie.Id}']");
                newMovie.Description = node.Attributes["Description"].Value;
                newMovie.UrlImage = node.Attributes["UrlImage"].Value;
                newMovie.UrlTrailer = node.Attributes["UrlTrailer"].Value;
            }
            DataProvider.Instance.Close();
        }

        public List<Customer> LoadCustomers()
        {
            List<Customer> customers = new List<Customer>();
            DataProvider.Instance.pathData = Constants.fCustomers;
            DataProvider.Instance.Open();
            XmlNodeList lstNode = DataProvider.Instance.nodeRoot.ChildNodes;

            foreach (XmlNode customerNode in lstNode)
            {
                Customer newCustomer = new Customer
                {
                    Id = customerNode.Attributes["Id"].Value,
                    Name = customerNode.Attributes["Name"].Value,
                    Phone = customerNode.Attributes["Phone"].Value
                };
                customers.Add(newCustomer);
            }
            DataProvider.Instance.Close();
            return customers;
        }

        public List<Order> LoadOrders(RepositoryBase<Cinema> cinemaRepo, RepositoryBase<Customer> customerRepo)
        {
            // Temporary code
            CinemaViewModel cinemaVM = new CinemaViewModel();
            cinemaVM.getList(cinemaRepo);
            CustomerViewModel customerVM = new CustomerViewModel();
            customerVM.getList(customerRepo);

            List<Order> lstOrder = new List<Order>();
            DataProvider.Instance.pathData = Constants.fOrders;
            DataProvider.Instance.Open();
            XmlNodeList lstNode = DataProvider.Instance.nodeRoot.ChildNodes;

            foreach (XmlNode node in lstNode)
            {
                Order newOrder = new Order();
                try
                {   
                    newOrder.Id = node.Attributes["Id"].Value;
                    newOrder.Customer.Id = node.Attributes["CustomerId"].Value;
                    newOrder.MovieOrder.Cinema.Id = node.Attributes["Cinema"].Value;
                    newOrder.OtherPrice = Convert.ToDouble(node.Attributes["OtherPrice"].Value);
                    newOrder.PayMent = Convert.ToDouble(node.Attributes["PayMent"].Value);
                    newOrder.Date = Convert.ToDateTime(node.Attributes["Date"].Value);

                    lstOrder.Add(newOrder);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                // Get additional data
                Cinema cinemaFind = cinemaVM.FindById(newOrder.MovieOrder.Cinema.Id);
                newOrder.MovieOrder.Cinema = cinemaFind;

                Customer customerFind = customerVM.FindById(newOrder.Customer.Id);
                newOrder.Customer = customerFind;

                List<OrderDetail> details = LoadOrderDetails(newOrder.MovieOrder.Cinema.Seats, newOrder.Id);
                newOrder.Details = details;
            }
            DataProvider.Instance.Close();
            return lstOrder;
        }

        public List<OrderDetail> LoadOrderDetails(List<List<Seat>> seats, string idOrder)
        {
            SeatViewModel seatVM = new SeatViewModel();
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            DataProvider.Instance.pathData = Constants.fOrderDetails;
            DataProvider.Instance.Open();
            XmlNode nodeTemp = DataProvider.Instance.getNode($"OrderDetails[@IdOrder='{idOrder}']");
            if (nodeTemp == null)
                return null;
            XmlNodeList lstNode = nodeTemp.ChildNodes;

            foreach (XmlNode orderDetailNode in lstNode)
            {
                OrderDetail newOrderDetail = new OrderDetail();
                try
                {
                    newOrderDetail.Id = orderDetailNode.Attributes["Id"].Value;
                    newOrderDetail.IdOrder = idOrder;
                    newOrderDetail.BookedSeat.Id = orderDetailNode.Attributes["BookedSeatId"].Value;
                    newOrderDetail.TicketType = (TicketType)Enum.Parse(typeof(TicketType), orderDetailNode.Attributes["TicketType"].Value);

                    newOrderDetail.PayMent.DiscountRef = Convert.ToDouble(orderDetailNode.Attributes["DiscountRef"].Value);
                    newOrderDetail.PayMent.Discount = Convert.ToDouble(orderDetailNode.Attributes["Discount"].Value);
                    newOrderDetail.PayMent.Price = Convert.ToDouble(orderDetailNode.Attributes["TotalPrice"].Value);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return null;
                }
                seatVM.seatRepo.Items = seats;
                Seat seatFind = seatVM.FindById(newOrderDetail.BookedSeat.Id);
                
                newOrderDetail.BookedSeat = seatFind;
                orderDetails.Add(newOrderDetail);
            }
            DataProvider.Instance.Close();
            return orderDetails;
        }

        public List<MovieSchedule> LoadMovieSchedules(RepositoryBase<Movie> movieRepo, RepositoryBase<Cinema> cinemaRepo)
        {
            List<MovieSchedule> lstMovieSchedule = new List<MovieSchedule>();

            MovieViewModel movieVM = new MovieViewModel();
            movieVM.getList(movieRepo);
            CinemaViewModel cinemaVM = new CinemaViewModel();
            cinemaVM.getList(cinemaRepo);

            DataProvider.Instance.pathData = Constants.fMovieSchedules;
            DataProvider.Instance.Open();

            XmlNodeList lstNodeMovie = DataProvider.Instance.nodeRoot.ChildNodes;

            MovieScheduleViewModel movieScheduleVM2 = new MovieScheduleViewModel();
            MovieViewModel movieVM2 = new MovieViewModel();
            movieScheduleVM2.MovieScheduleRepo.Items = lstMovieSchedule;
            foreach (XmlNode nodeMovie in lstNodeMovie)
            {
                movieVM2.MovieRepo.Items = movieScheduleVM2.FillMovie();
                string idMovie  = nodeMovie.Attributes["Movie"].Value;
                Movie movieCheck = movieVM2.FindById(idMovie);
                if (movieCheck != null)
                {
                    Utilities.HandleError();
                }
                try
                {
                    MovieSchedule newMovieSchedule = new MovieSchedule();
                    newMovieSchedule.Movie.Id = nodeMovie.Attributes["Movie"].Value;
                    Movie movieFind = movieVM.FindById(newMovieSchedule.Movie.Id);
                    newMovieSchedule.Movie = movieFind;

                    XmlNodeList lstNodeCinemaType = nodeMovie.SelectSingleNode("CinemaTypeSchedules").ChildNodes;
                    foreach (XmlNode nodeCinemaType in lstNodeCinemaType)
                    {
                        CinemaTypeSchedule newCinemaTypeSchedule = new CinemaTypeSchedule();
                        string strType = nodeCinemaType.Attributes["Type"].Value;
                        newCinemaTypeSchedule.CinemaType = (CinemaType) Enum.Parse(typeof(CinemaType), strType);

                        XmlNodeList lstNodeCinemaSchedule = nodeCinemaType.SelectSingleNode("CinemaSchedules").ChildNodes;
                        foreach (XmlNode nodeCinemaSchedule in lstNodeCinemaSchedule)
                        {
                            CinemaSchedule newCinemaSchedule = new CinemaSchedule();
                            newCinemaSchedule.Cinema.Id = nodeCinemaSchedule.Attributes["Cinema"].Value;
                            Cinema cinemaFind = cinemaVM.FindById(newCinemaSchedule.Cinema.Id);
                            newCinemaSchedule.Cinema = cinemaFind;

                            XmlNodeList lstNodeDate = nodeCinemaSchedule.SelectSingleNode("DateSchedules").ChildNodes;
                            foreach (XmlNode nodeDate in lstNodeDate)
                            {
                                DateSchedule newDate = new DateSchedule();
                                newDate.Date = Convert.ToDateTime(nodeDate.Attributes["Date"].Value);

                                XmlNodeList lstNodeTime = nodeDate.SelectSingleNode("TimeSchedules").ChildNodes;
                                foreach (XmlNode nodeTime in lstNodeTime)
                                {
                                    TimeSchedule timeSchedule = new TimeSchedule();
                                    timeSchedule.Time = TimeSpan.Parse(nodeTime.Attributes["Time"].Value);

                                    newDate.TimeShedules.Add(timeSchedule);
                                }
                                newCinemaSchedule.DatesSchedule.Add(newDate);
                            }
                            newCinemaTypeSchedule.CinemaSchedules.Add(newCinemaSchedule);
                        }
                        newMovieSchedule.CinemaTypeSchedules.Add(newCinemaTypeSchedule);
                    }
                    lstMovieSchedule.Add(newMovieSchedule);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            DataProvider.Instance.Close();

            LoadSeatsSchedules(lstMovieSchedule);
            return lstMovieSchedule;
        }

        public void LoadSeatsSchedules(List<MovieSchedule> lstMovieSchedule)
        {
            SeatViewModel seatVM = new SeatViewModel();
            foreach (var movie in lstMovieSchedule)
            {
                List<CinemaTypeSchedule> cinemaTypeSchedules = movie.CinemaTypeSchedules;
                foreach (var cinemaType in cinemaTypeSchedules)
                {
                    List<CinemaSchedule> cinemaSchedules = cinemaType.CinemaSchedules;
                    foreach (var cinema in cinemaSchedules)
                    {
                        List<DateSchedule> dateSchedules = cinema.DatesSchedule;
                        foreach (var date in dateSchedules)
                        {
                            List<TimeSchedule> timeSchedules = date.TimeShedules;
                            foreach (var time in timeSchedules)
                            {
                                string filePath = Environment.CurrentDirectory + "\\Data\\MovieSchedules";
                                filePath += "\\" + movie.Movie.Id;
                                filePath += "\\" + cinemaType.CinemaType;
                                filePath += "\\" + cinema.Cinema.Id;
                                filePath += "\\" + date.Date.ToString(Constants.formatDateFile);
                                filePath += "\\" + time.Time.ToString(Constants.formatTimeFile) + ".xml";

                                DataProvider.Instance.pathData = filePath;
                                try
                                {
                                    DataProvider.Instance.Open();
                                }
                                catch
                                {
                                    Utilities.HandleError();
                                }
                                XmlNode root = DataProvider.Instance.nodeRoot;
                                seatVM.GetListSeat(time.Seats, root.ChildNodes);
                                seatVM.SetSeatsPrice(time.Seats, cinema.Cinema.PriceCenter, cinema.Cinema.DecreasePrice);

                                DataProvider.Instance.Close();
                            }
                        }
                    }
                }
            }
        }

        public void LoadSeatsCinemas(List<Cinema> cinemas)
        {
            SeatViewModel seatVM = new SeatViewModel();
            foreach (var cinema in cinemas)
            {
                DataProvider.Instance.pathData = Constants.fCinemas;
                try
                {
                    DataProvider.Instance.Open();
                }
                catch
                {
                    Utilities.HandleError();
                }
                XmlNode root = DataProvider.Instance.nodeRoot;
                XmlNode cinemaNode = root.SelectSingleNode($"Cinema[@Id='{cinema.Id}']");
                XmlNodeList lstSeatNode = cinemaNode.FirstChild.ChildNodes;
                seatVM.GetListSeat(cinema.Seats, lstSeatNode);
                seatVM.SetSeatsPrice(cinema.Seats, cinema.PriceCenter, cinema.DecreasePrice);

                DataProvider.Instance.Close();
            }
        }
    }
}
