using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AnhQuoc_WPF_C1_B1
{
    /// <summary>
    /// Interaction logic for ucViewTicketPrice.xaml
    /// </summary>
    public partial class ucViewTicketPrice : UserControl
    {
        public Func<RepositoryBase<Cinema>> getCinemaRepo { get; set; }

        private ObservableCollection<Cinema> _cinemas;
        private string prefixName = "btnSeat";
        private List<List<Button>> _arrayBtn;

        private List<Seat> _bookedSeats;
        private List<Seat> _totalBookedSeats;

        private int s = 40;
        private int _padding = 5;
        private int _margin = 5;
        private Color btnSeatColor = Colors.LightCyan;
        private int row = 5;
        private int col = 5;

        public ucViewTicketPrice()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _cinemas = new ObservableCollection<Cinema>(getCinemaRepo().Gets());
            cbCinema.ItemsSource = _cinemas;

            cbCinema.DisplayMemberPath = "Name";
            cbCinema.SelectedValuePath = "Name";

            _bookedSeats = new List<Seat>();
            _totalBookedSeats = new List<Seat>();

            cbCinema.SelectedIndex = 0;
        }

        public void Init(List<List<Button>> source, Cinema cinema)
        {
            int idx = 0;
            int idx2 = 0;
            foreach (List<Button> items in source)
            {
                foreach (Button item in items)
                {
                    item.BorderThickness = new Thickness(2);
                    item.Content = cinema.Seats[idx][idx2].Id;
                    item.Name = prefixName + cinema.Seats[idx][idx2].Id;
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
            Button item = sender as Button;
            CinemaViewModel cinemaVM = new CinemaViewModel();
            cinemaVM.CinemaRepo = getCinemaRepo();

            Cinema selectedCinema = cbCinema.SelectedItem as Cinema;

            SeatViewModel seatVM = new SeatViewModel();
            seatVM.seatRepo.Items = selectedCinema.Seats;
            Seat seatBooked = seatVM.FindById(item.Content.ToString());

            MessageBox.Show(seatBooked.Price.ToString() + " VND");
        }

        private void cbCinema_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Cinema selectedCinema = cbCinema.SelectedItem as Cinema;
            if (selectedCinema == null)
                return;
            Utilities.Allocate(out _arrayBtn, row, col);
            Init(_arrayBtn, selectedCinema);
            AddToGroupBox(_arrayBtn);
        }

        private Button FindButtonByName(string nameValue)
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
    }
}
