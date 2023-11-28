using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml;

namespace AnhQuoc_WPF_C1_B1
{
    /// <summary>
    /// Interaction logic for ucSeatBooking.xaml
    /// </summary>
    public partial class ucSeatBooking : UserControl, INotifyPropertyChanged
    {
        public Func<RepositoryBase<Order>> getOrderRepo { get; set; }
        public Func<RepositoryBase<Customer>> getCustomerRepo { get; set; }
        public Func<List<List<Seat>>> getSeats { get; set; }
        public Func<ucBooking> getUcBooking { get; set; }
    
        private Func<MessageBoxResult> getMessage = () 
            => MessageBox.Show("Cancel the booking", "Booking infomation", 
               MessageBoxButton.OKCancel, MessageBoxImage.Information, MessageBoxResult.Cancel);

        private List<Seat> _checkedSeats;
        private List<OrderDetail> _orderDetails;
        private List<List<Seat>> seats;

        // Constants variable
        public string prefixName = "btnSeat";
        public Color btnSeatColor = Colors.LightCyan;
        public Color btnBookedSeatColor = Colors.Red;

        // Seat display
        private List<List<Button>> _arrayBtn;
        private int row = 5;
        private int col = 5;
        private int s = 40;
        private int _padding = 5;
        private int _margin = 5;

        #region TotalPriceBooked
        // Set Property changed to TotalPrice for Binding
        private double _TotalPriceBooked;
        public double TotalPriceBooked
        {
            get { return _TotalPriceBooked; }
            set
            {
                _TotalPriceBooked = value;
                OnPropertyChanged("TotalPriceBooked");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private OrderDetail _CurrentSelected;
        public OrderDetail CurrentSelected
        {
            get { return _CurrentSelected; }
            set
            {
                _CurrentSelected = value;
                OnPropertyChanged("CurrentSelected");
            }
        }

        public ucSeatBooking()
        {
            InitializeComponent();

            _checkedSeats = new List<Seat>();
            _orderDetails = new List<OrderDetail>();

            TotalPriceBooked = 0;

            _arrayBtn = new List<List<Button>>();

            this.DataContext = this;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            seats = getSeats();
            LoadSeat();
        }

        private void LoadSeat()
        {
            Utilities.Allocate(out _arrayBtn, row, col);
            Init(_arrayBtn, seats);
            SetSeatColor(_arrayBtn);
            AddToGroupBox(_arrayBtn);

            _checkedSeats.Clear();
            TotalPriceBooked = 0;

            _orderDetails.Clear();
        }

        public OrderDetail GetOrderDetail(Seat bookSeat, string idOrder)
        {
            OrderDetailViewModel orderDetailVM = new OrderDetailViewModel();
            OrderDetail newDetail = new OrderDetail();

            int _noDetail = _orderDetails.Count + 1;
            newDetail.Id = orderDetailVM.GetId(_noDetail);
            newDetail.IdOrder = idOrder;
            newDetail.BookedSeat = bookSeat;

            newDetail.PayMent.DiscountRef = 0;
            newDetail.PayMent.Discount = bookSeat.Price * (newDetail.PayMent.DiscountRef / 100.0);
            newDetail.PayMent.Price = bookSeat.Price - newDetail.PayMent.Discount;
           
            return newDetail;
        }

        public void Init(List<List<Button>> source, List<List<Seat>> seats)
        {
            int idx = 0;
            int idx2 = 0;
            foreach (List<Button> items in source)
            {
                foreach (Button item in items)
                {
                    item.BorderThickness = new Thickness(2);
                    item.Content = seats[idx][idx2].Id;
                    item.Name = prefixName + seats[idx][idx2].Id;
                    item.Width = s;
                    item.Height = s;
                    item.Margin = new Thickness(_margin);
                    item.Padding = new Thickness(_padding);
                    item.Background = new SolidColorBrush(btnSeatColor);
                    idx2 += 1;
                }
                idx += 1;
                idx2 = 0;
            }
        }

        public void SetSeatColor(List<List<Button>> source)
        {
            SeatViewModel seatVM = new SeatViewModel();
            seatVM.seatRepo.Items = seats;

            foreach (List<Button> items in source)
            {
                foreach (Button item in items)
                {
                    Seat getSeat = seatVM.FindById(item.Content.ToString());
                    if (getSeat == null) return;
                    if (getSeat.IsBooked == true)
                    {
                        item.Background = new SolidColorBrush(btnBookedSeatColor);
                    }
                }
            }
        }

        private void DefineRowColDefinitions(Grid grid1, int row, int col)
        {
            double size = s + (_margin * 2);
            for (int idx = 0; idx < row; idx++)
            {
                RowDefinition rowDef = new RowDefinition();
                rowDef.Height = new GridLength(size, GridUnitType.Pixel);
                grid1.RowDefinitions.Add(rowDef);
            }
            for (int idx = 0; idx < col; idx++)
            {
                ColumnDefinition colDef = new ColumnDefinition();
                colDef.Width = new GridLength(size, GridUnitType.Pixel);
                grid1.ColumnDefinitions.Add(colDef);
            }
        }

        private void AddToGroupBox(List<List<Button>> _arrayBtn)
        {
            Grid grid1 = new Grid();
            DefineRowColDefinitions(grid1, row, col);

            int idx = 0;
            int idx2 = 0;
            foreach (List<Button> lstBtn in _arrayBtn)
            {
                foreach (Button btn in lstBtn)
                {
                    btn.Click += BtnSeat_Click;
                    grid1.Children.Add(btn);

                    Grid.SetRow(btn, idx);
                    Grid.SetColumn(btn, idx2);

                    idx2 += 1;
                }
                idx += 1;
                idx2 = 0;
            }
            gbSeats.Content = grid1;
        }

        private void BtnSeat_Click(object sender, RoutedEventArgs e)
        {
            CinemaViewModel cinemaVM = new CinemaViewModel();
            SeatViewModel seatVM = new SeatViewModel();
            seatVM.seatRepo.Items = seats;
            // cinemaVM.CinemaRepo = getCinemaRepo();
            Button item = sender as Button;

            Seat seatBooked = seatVM.FindById(item.Content.ToString());
            if (seatBooked.IsBooked)
            {
                MessageBox.Show("This seat has been reserved");
            }
            else
            {
                if (!_checkedSeats.Contains(seatBooked))
                {
                    item.Background = new SolidColorBrush(Colors.Green);
                    _checkedSeats.Add(seatBooked);

                    CurrentSelected = GetOrderDetail(seatBooked, getUcBooking().newOrder.Id);
                    _orderDetails.Add(CurrentSelected);

                    EnumViewModel enumVM = new EnumViewModel();
                    cbTicketType.ItemsSource = new ObservableCollection<TicketType>(enumVM.GetValues<TicketType>());
                    cbTicketType.SelectedIndex = 0;
                    cbTicketType.SelectionChanged += CbTicketType_SelectionChanged;
                }
                else
                {
                    item.Background = new SolidColorBrush(btnSeatColor);
                    _checkedSeats.Remove(seatBooked);
                    TotalPriceBooked -= seatBooked.Price;

                    OrderDetailViewModel orderDetailVM = new OrderDetailViewModel();
                    orderDetailVM.OrderDetailRepo = new RepositoryBase<OrderDetail>();
                    orderDetailVM.OrderDetailRepo.Items = _orderDetails;

                    CurrentSelected = orderDetailVM.FindBySeatId(seatBooked.Id);
                    _orderDetails.Remove(CurrentSelected);
                }
            }
        }

        private void CbTicketType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TicketType item = TicketType.Adult;
            try
            {
                item = (TicketType)cbTicketType.SelectedItem;
            }
            catch { }
            if (item == TicketType.Children)
            {
                CurrentSelected.PayMent.DiscountRef = 50;
            }
            else if (item == TicketType.Adult)
            {
                CurrentSelected.PayMent.DiscountRef = 0;
            }
            CurrentSelected.PayMent.Discount = CurrentSelected.BookedSeat.Price * (CurrentSelected.PayMent.DiscountRef / 100.0);
            CurrentSelected.PayMent.Price = CurrentSelected.BookedSeat.Price - CurrentSelected.PayMent.Discount;
        }

        public Button FindButtonByName(string nameValue)
        {
            foreach (List<Button> btnRow in _arrayBtn)
            {
                foreach (Button btn in btnRow)
                {
                    if (nameValue == btn.Name)
                    {
                        return btn;
                    }
                }
            }
            return null;
        }
        
        private bool IsCheck()
        {
            // Checking _bookSeats
            if (_checkedSeats.Count == 0)
            {
                MessageBox.Show("Please choose seat to book!");
                return false;
            }
            return true;
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (!IsCheck())
            {
                return;
            }
            if (getUcBooking == null)
                return;

            InputOrder();

            bool frmReply = PrintOrder();
            if (frmReply)
            {
                SaveBooking(getUcBooking().newOrder);
            }
            else
            {
                btnCancel_Click(null, null);
                return;
            }
            getUcBooking().Content = null;
        }
        
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msgResult = getMessage();
            if (msgResult == MessageBoxResult.OK)
            {
                getUcBooking().Content = null;
            }
        }

        private void InputOrder()
        {
            Order newOrder = getUcBooking().newOrder;
            newOrder.Details = _orderDetails;
            newOrder.Date = DateTime.Now;
            if (newOrder.MovieOrder.Cinema.Type == CinemaType.VIP)
                newOrder.OtherPrice += 40000;

            newOrder.PayMent = GetTotalPrice() + newOrder.OtherPrice;
        }

        private double GetTotalPrice()
        {
            double result = 0;
            foreach (OrderDetail detail in _orderDetails)
            {
                result += detail.PayMent.Price;
            }
            return result;
        }

        private bool PrintOrder()
        {
            frmOrder frmOrder = new frmOrder();
            frmOrder.getOrder = () => getUcBooking().newOrder;
            frmOrder.ShowDialog();
            return frmOrder.frmReply;
        }

        private void ResetBooking(Color seatColor)
        {
            foreach (Seat bookedSeat in _checkedSeats)
            {
                string btnNameValue = prefixName + bookedSeat.Id;
                Button btnFinded = FindButtonByName(btnNameValue);
                btnFinded.Background = new SolidColorBrush(seatColor);
            }
            _checkedSeats.Clear();
            TotalPriceBooked = 0;
        }

        public void SaveBooking(Order newOrder)
        {
            OrderViewModel orderVM = new OrderViewModel();
            OrderDetailViewModel orderDetailVM = new OrderDetailViewModel();
            CustomerViewModel customerVM = new CustomerViewModel();

            OrderViewModel orderVMMain = new OrderViewModel();
            orderVMMain.OrderRepo = getOrderRepo();
            
            // Save to list
            orderVMMain.OrderRepo.Add(newOrder);
            getCustomerRepo().Add(newOrder.Customer);

            foreach (OrderDetail detail in newOrder.Details)
            {
                Seat seatBooked = detail.BookedSeat;
                seatBooked.IsBooked = true;
            }

            XmlNode parentNode = null;

            // Set seat status
            string filePath = Environment.CurrentDirectory + "\\Data\\MovieSchedules";
            filePath += "\\" + newOrder.MovieOrder.Movie.Id;
            filePath += "\\" + newOrder.MovieOrder.CinemaType.ToString();
            filePath += "\\" + newOrder.MovieOrder.Cinema.Id;
            filePath += "\\" + newOrder.MovieOrder.Date.ToString(Constants.formatDateFile);
            filePath += "\\" + newOrder.MovieOrder.Time.ToString(Constants.formatTimeFile) + ".xml";

            DataProvider.Instance.pathData = filePath;
            DataProvider.Instance.Open();
            parentNode = DataProvider.Instance.nodeRoot;

            foreach (OrderDetail detail in newOrder.Details)
            {
                Seat seatBooked = detail.BookedSeat;
                XmlNode seatNode = parentNode.SelectSingleNode($"Seat[@Id='{seatBooked.Id}']");
                seatNode.Attributes["IsBooked"].Value = "1";
            }

            DataProvider.Instance.Close();

            DataProvider.Instance.pathData = Constants.fOrders;
            DataProvider.Instance.Open();
            parentNode = DataProvider.Instance.nodeRoot;
            XmlNode addNode = orderVM.Write(newOrder);
            XmlNode detailNode = DataProvider.Instance.createNode("OrderDetails");
            orderDetailVM.Write(detailNode, newOrder.Details, newOrder.Id);
            addNode.AppendChild(detailNode);
            parentNode.AppendChild(addNode);
            DataProvider.Instance.Close();

            DataProvider.Instance.pathData = Constants.fOrderDetails;
            DataProvider.Instance.Open();
            parentNode = DataProvider.Instance.nodeRoot;
            detailNode = DataProvider.Instance.createNode("OrderDetails");
            orderDetailVM.Write(detailNode, newOrder.Details, newOrder.Id);
            parentNode.AppendChild(detailNode);
            DataProvider.Instance.Close();

            DataProvider.Instance.pathData = Constants.fCustomers;
            DataProvider.Instance.Open();
            parentNode = DataProvider.Instance.nodeRoot;
            parentNode.AppendChild(customerVM.Write(newOrder.Customer));
            DataProvider.Instance.Close();

            MessageBox.Show("Booking successfully!", "Booking infomation", MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);

            ResetBooking(btnBookedSeatColor);
            _checkedSeats = new List<Seat>();
        }
    }
}
