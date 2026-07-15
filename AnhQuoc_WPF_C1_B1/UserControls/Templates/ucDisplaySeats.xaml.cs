using AnhQuoc_WPF_C1_B1.Enums;
using AnhQuoc_WPF_C1_B1.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
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
using System.Xml;

namespace AnhQuoc_WPF_C1_B1.UserControls
{
    /// <summary>
    /// Interaction logic for ucDisplaySeats.xaml
    /// </summary>
    public partial class ucDisplaySeats : UserControl
    {
        #region GetData



        public frmBookingTicket frmBookingTicket
        {
            get { return (frmBookingTicket)GetValue(frmBookingTicketProperty); }
            set { SetValue(frmBookingTicketProperty, value); }
        }

        // Using a DependencyProperty as the backing store for frmBookingTicket.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty frmBookingTicketProperty =
            DependencyProperty.Register(nameof(frmBookingTicket), typeof(frmBookingTicket), typeof(ucDisplaySeats), new PropertyMetadata(null));




        public Cinema Cinema
        {
            get { return (Cinema)GetValue(CinemaProperty); }
            set { SetValue(CinemaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Cinema.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CinemaProperty =
            DependencyProperty.Register(nameof(Cinema), typeof(Cinema), typeof(ucDisplaySeats), new PropertyMetadata(null));


        #endregion

        #region Fields
        public ObservableCollection<Seat> SelectedSeats { get; set; } = new ObservableCollection<Seat>();
        #endregion

        public ObservableCollection<Seat> Seats { get; set; } = new ObservableCollection<Seat>();

        public ucDisplaySeats()
        {
            InitializeComponent();

            SelectedSeats.CollectionChanged += SelectedSeats_CollectionChanged;
            this.Loaded += UcDisplaySeats_Loaded;
        }

        private void SelectedSeats_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ucTicketSummary.SelectedSeats_CollectionChanged(sender, e);
        }

        private void UcDisplaySeats_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData(Cinema.Id);
            this.DataContext = this;
        }

        private void SaveData(string idCinema)
        {
            DataProvider.Instance.pathData = Constants.fCinemas;
            DataProvider.Instance.Open();

            XmlNode root = DataProvider.Instance.nodeRoot;
            XmlNode cinemaNode = root.SelectSingleNode($"Cinema[@Id='{idCinema}']");
            
            XmlNode listSeatNode = cinemaNode.SelectSingleNode($"Seats");
            if (listSeatNode == null)
            {
                listSeatNode = DataProvider.Instance.createNode("Seats");
                root.FirstChild.AppendChild(listSeatNode);
            }

            foreach (var seat in Seats)
            {
                if (seat == null)
                    continue;
                XmlNode newNode = DataProvider.Instance.createNode("Seat");

                // Get all public instance properties of the seat object
                PropertyInfo[] properties = seat.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo property in properties)
                {
                    // Skip properties that cannot be read (just in case)
                    if (!property.CanRead) continue;

                    // Get the current value of the property from your seat object
                    object value = property.GetValue(seat);

                    // Only serialize if the value is not null
                    if (value != null)
                    {
                        // 1. Create the XML Attribute using its C# property name (e.g., "Id", "Price", "Type")
                        XmlAttribute newAttr = DataProvider.Instance.createAttr(property.Name);

                        // 2. Assign the string representation of the value
                        newAttr.Value = value.ToString();

                        // 3. Append it to the node's attributes collection
                        newNode.Attributes.Append(newAttr);
                    }
                }
                listSeatNode.AppendChild(newNode);

            }
            DataProvider.Instance.Close();
        }

        private void LoadData(string idCinema)
        {
            // 1. Initialize or clear your Seats collection
            Seats.Clear();

            // 2. We need to pre-fill the Seats collection with nulls up to the total grid size 
            // (9 rows * 18 columns = 162 total slots). This acts as our empty canvas.
            int totalRows = 9;
            int totalVisualColumns = 18;
            int totalGridSize = totalRows * totalVisualColumns;

            for (int i = 0; i < totalGridSize; i++)
            {
                Seats.Add(null);
            }

            // 3. Open the XML file
            DataProvider.Instance.pathData = Constants.fCinemas;
            DataProvider.Instance.Open();

            XmlNode root = DataProvider.Instance.nodeRoot;

            XmlNode cinemaNode = root.SelectSingleNode($"Cinema[@Id='{idCinema}']");
            XmlNode listSeatNode = cinemaNode.SelectSingleNode("Seats");

            if (listSeatNode != null)
            {
                foreach (XmlNode node in listSeatNode.ChildNodes)
                {
                    if (node.Name != "Seat" || node.Attributes == null) continue;

                    // 4. Reconstruct the Seat object dynamically from its attributes
                    Seat seat = new Seat();

                    // Extract critical layout indices
                    int rowIndex = int.Parse(node.Attributes["RowIndex"]?.Value ?? "0");
                    int columnIndex = int.Parse(node.Attributes["ColumnIndex"]?.Value ?? "0");

                    // Fill all properties using reflection (matching how you saved it)
                    PropertyInfo[] properties = typeof(Seat).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    foreach (PropertyInfo property in properties)
                    {
                        if (!property.CanWrite) continue;

                        XmlAttribute attr = node.Attributes[property.Name];
                        if (attr != null)
                        {
                            // Convert the string back to the correct property type (int, string, SeatType, etc.)
                            object convertedValue;
                            if (property.PropertyType.IsEnum)
                            {
                                convertedValue = Enum.Parse(property.PropertyType, attr.Value);
                            }
                            else
                            {
                                convertedValue = Convert.ChangeType(attr.Value, property.PropertyType);
                            }

                            property.SetValue(seat, convertedValue);
                        }
                    }

                    // 5. Calculate the exact 1D array index and overwrite the null placeholder
                    int target1DIndex = (rowIndex * totalVisualColumns) + columnIndex;

                    if (target1DIndex < Seats.Count)
                    {
                        Seats[target1DIndex] = seat;
                    }
                }
            }

            DataProvider.Instance.Close();
        }

        private void GenerateLayout()
        {
            string[] rows = { "A", "B", "C", "D", "E", "F", "G", "H", "I" };

            // The visual layout consists of 14 actual seat columns + 4 aisle spacing columns = 18 total grid indices.
            int totalVisualColumns = 18;

            for (int r = 0; r < rows.Length; r++)
            {
                for (int c = 0; c < totalVisualColumns; c++)
                {
                    // 1. Inject Pure Aisle Spacers (Visual columns: 2, 5, 10, 15)
                    if (c == 2 || c == 5 || c == 10 || c == 15)
                    {
                        Seats.Add(null); // Added directly to the 1D collection
                        continue;
                    }

                    // 2. Map the absolute visual column index back to the natural seat number (1 to 14)
                    int naturalSeatNumber = GetNaturalSeatNumber(c);

                    // 3. Skip seats 13 and 14 for rows A through G (the isolated bottom-right section)
                    if (r < 7 && (naturalSeatNumber == 13 || naturalSeatNumber == 14))
                    {
                        Seats.Add(null);
                        continue;
                    }

                    // 4. Remove seat A12 (Top-right structural/architectural corner gap)
                    if (r == 0 && naturalSeatNumber == 12)
                    {
                        Seats.Add(null);
                        continue;
                    }

                    // 5. Determine Seat Color Tier Categories
                    SeatType sType = SeatType.Standard; // Default Purple color

                    // Central VIP Area (Rows D to G entirely, plus the surrounding blocks)
                    if ((r >= 3 && r <= 6 && naturalSeatNumber >= 3 && naturalSeatNumber <= 12) ||
                        (r == 7 && naturalSeatNumber >= 1 && naturalSeatNumber <= 12))
                    {
                        sType = SeatType.VIP; // Red color
                    }

                    // Hardcoded Booked Seats (Gray seats at H7 and H8)
                    if (r == 7 && (naturalSeatNumber == 7 || naturalSeatNumber == 8))
                    {
                        sType = SeatType.Booked; // Gray color
                    }

                    Seat newSeat = new Seat
                    {
                        Id = $"{rows[r]}{naturalSeatNumber}",
                        Type = sType,
                        RowIndex = r,
                        ColumnIndex = c
                    };
                    if (sType == SeatType.Standard)
                        newSeat.Price = Constants.StandardSeatPrice;
                    else
                        newSeat.Price = Constants.VIPSeatPrice;


                    // 6. Append the verified seat node directly to our 1D collection
                    Seats.Add(newSeat);

                    //          0, 1, 3 (Không càn quá chính xác)
                }
            }
        }

        private int GetNaturalSeatNumber(int visualGridIndex)
        {
            switch (visualGridIndex)
            {
                case 0: return 1;
                case 1: return 2;
                // Index 2 is an Aisle Gap
                case 3: return 3;
                case 4: return 4;
                // Index 5 is an Aisle Gap
                case 6: return 5;
                case 7: return 6;
                case 8: return 7;
                case 9: return 8;
                // Index 10 is an Aisle Gap
                case 11: return 9;
                case 12: return 10;
                case 13: return 11;
                case 14: return 12;
                // Index 15 is a Large Aisle Gap
                case 16: return 13;
                case 17: return 14;
                default: return 0;
            }
        }

        private void btnSeat_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                // This gets the specific object bound to this button row/item
                var clickedItem = button.DataContext;
                if (clickedItem is Seat seat)
                {
                    if (seat.Type != SeatType.Selected)
                    {
                        Seat coppiedSeat = new Seat();
                        Utilities.CopyProperties(seat, coppiedSeat);

                        SelectedSeats.Add(coppiedSeat);
                        seat.Type = SeatType.Selected;

                        ucTicketSummary.EstimatedPrice += seat.Price;
                    }
                    else
                    {
                        Seat removedSeat = SelectedSeats.FirstOrDefault(item => item.Id ==  seat.Id);
                        if (removedSeat == null)
                            return;
                        seat.Type = removedSeat.Type;
                        SelectedSeats.Remove(removedSeat);
                    }
                }
            }
        }
    }
}
