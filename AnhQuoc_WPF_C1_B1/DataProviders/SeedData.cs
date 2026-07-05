using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Linq;

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

                    newAccount.Image = node.Attributes["Image"].Value;
                    newAccount.Username = node.Attributes["Username"].Value;
                    newAccount.Password = node.Attributes["Password"].Value;
                    newAccount.Email = node.Attributes["Email"].Value;
                    newAccount.Phone = node.Attributes["Phone"].Value;
                    newAccount.Address = node.Attributes["Address"].Value;
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
            // LoadSeatsCinemas(cinemas);
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
                if (seats != null)
                {
                    seatVM.seatRepo.Items = seats;
                    Seat seatFind = seatVM.FindById(newOrderDetail.BookedSeat.Id);
                    newOrderDetail.BookedSeat = seatFind;
                }
                orderDetails.Add(newOrderDetail);
            }
            DataProvider.Instance.Close();
            return orderDetails;
        }

        public List<OrderDetail> LoadAllOrderDetails(List<List<Seat>> seats, RepositoryBase<Order> orderRepo)
        {
            List<OrderDetail> allOrderDetails = new List<OrderDetail>();
            foreach (Order order in orderRepo.Items)
            {
                List<OrderDetail> orderDetails = LoadOrderDetails(seats, order.Id);
                if (orderDetails != null)
                {
                    allOrderDetails.AddRange(orderDetails);
                }
            }
            return allOrderDetails;
        }

        public List<MovieSchedule> LoadMovieSchedules(RepositoryBase<Movie> movieRepo, RepositoryBase<Cinema> cinemaRepo)
        {
            MovieViewModel movieViewModel = new MovieViewModel();
            movieViewModel.MovieRepo = movieRepo;
            CinemaViewModel cinemaViewModel = new CinemaViewModel();
            cinemaViewModel.CinemaRepo = cinemaRepo;

            // Initialize the result list so we aren't returning null on success
            List<MovieSchedule> result = new List<MovieSchedule>();

            DataProvider.Instance.pathData = Constants.fMovieSchedules;
            DataProvider.Instance.Open();
            XmlNode root = DataProvider.Instance.nodeRoot;

            if (root == null) return result;

            foreach (XmlNode movieScheduleNode in root.ChildNodes)
            {
                // Safety check for comment nodes or whitespace text nodes
                if (movieScheduleNode.NodeType != XmlNodeType.Element) continue;

                MovieSchedule movieSchedule = new MovieSchedule();
                XmlAttribute attr = movieScheduleNode.Attributes["Movie"];

                if (attr == null) continue;

                Movie movieFinded = movieViewModel.FindById(attr.Value);
                if (movieFinded == null)
                {
                    Utilities.HandleError();
                    return null;
                }
                movieSchedule.Movie = movieFinded;
                movieSchedule.CinemaTypeSchedules = new List<CinemaTypeSchedule>();

                // 1. Parse <CinemaTypeSchedule>
                foreach (XmlNode cinemaTypeNode in movieScheduleNode.ChildNodes)
                {
                    if (cinemaTypeNode.NodeType != XmlNodeType.Element) continue;

                    CinemaTypeSchedule typeSchedule = new CinemaTypeSchedule();
                    XmlAttribute typeAttr = cinemaTypeNode.Attributes["CinemaType"];

                    if (typeAttr != null)
                    {
                        if (Enum.TryParse(typeAttr.Value, out CinemaType cinemaType))
                        {
                            typeSchedule.CinemaType = cinemaType;
                        }
                        else
                        {
                            Utilities.HandleError();
                            return null;
                        }
                    }
                    typeSchedule.CinemaSchedules = new List<CinemaSchedule>();

                    // 2. Parse <CinemaSchedule>
                    foreach (XmlNode cinemaScheduleNode in cinemaTypeNode.ChildNodes)
                    {
                        if (cinemaScheduleNode.NodeType != XmlNodeType.Element) continue;

                        CinemaSchedule cinemaSchedule = new CinemaSchedule();
                        XmlAttribute cinemaAttr = cinemaScheduleNode.Attributes["Cinema"];

                        if (cinemaAttr == null) continue;

                        Cinema cinemaFinded = cinemaViewModel.FindById(cinemaAttr.Value);
                        if (cinemaFinded == null)
                        {
                            Utilities.HandleError();
                            return null;
                        }
                        cinemaSchedule.Cinema = cinemaFinded;
                        cinemaSchedule.DatesSchedule = new List<DateSchedule>();

                        // 3. Parse <DateSchedule>
                        foreach (XmlNode dateScheduleNode in cinemaScheduleNode.ChildNodes)
                        {
                            if (dateScheduleNode.NodeType != XmlNodeType.Element) continue;

                            DateSchedule dateSchedule = new DateSchedule();
                            XmlAttribute dateAttr = dateScheduleNode.Attributes["Date"];

                            if (dateAttr == null) continue;

                            // Parse string "23/07/2026 12:00:00 SA" back to a DateTime object
                            // "SA/CH" suggests Vietnamese locale (AM/PM equivalent)
                            IFormatProvider culture = new System.Globalization.CultureInfo("vi-VN");
                            if (DateTime.TryParse(dateAttr.Value, culture, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
                            {
                                dateSchedule.Date = parsedDate;
                            }
                            else
                            {
                                // Fallback standard parse if culture parsing fails
                                DateTime.TryParse(dateAttr.Value, out parsedDate);
                                dateSchedule.Date = parsedDate;
                            }

                            dateSchedule.TimeSchedules = new List<TimeSchedule>();

                            // 4. Parse <TimeSchedule>
                            foreach (XmlNode timeScheduleNode in dateScheduleNode.ChildNodes)
                            {
                                if (timeScheduleNode.NodeType != XmlNodeType.Element) continue;

                                TimeSchedule timeSchedule = new TimeSchedule();
                                XmlAttribute timeAttr = timeScheduleNode.Attributes["Time"];

                                if (timeAttr != null && TimeSpan.TryParse(timeAttr.Value, out TimeSpan parsedTime))
                                {
                                    timeSchedule.Time = parsedTime;
                                    dateSchedule.TimeSchedules.Add(timeSchedule);
                                }
                            }
                            cinemaSchedule.DatesSchedule.Add(dateSchedule);
                        }
                        typeSchedule.CinemaSchedules.Add(cinemaSchedule);
                    }
                    movieSchedule.CinemaTypeSchedules.Add(typeSchedule);
                }
                result.Add(movieSchedule);
            }
            DataProvider.Instance.Close();
            return result;
        }

        public void LoadSeatsSchedules(List<MovieSchedule> lstMovieSchedule)
        {
            SeatViewModel seatVM = new SeatViewModel();
            foreach (var movie in lstMovieSchedule)
            {
                List<CinemaTypeSchedule> cinemaTypeSchedules = movie.CinemaTypeSchedules;
                foreach (var cinemaType in cinemaTypeSchedules)
                {
                    var cinemaSchedules = cinemaType.CinemaSchedules;
                    foreach (var cinema in cinemaSchedules)
                    {
                        List<DateSchedule> dateSchedules = cinema.DatesSchedule;
                        foreach (var date in dateSchedules)
                        {
                            List<TimeSchedule> timeSchedules = date.TimeSchedules;
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
