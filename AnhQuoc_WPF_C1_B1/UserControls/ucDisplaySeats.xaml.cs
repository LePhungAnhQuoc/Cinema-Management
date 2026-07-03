using AnhQuoc_WPF_C1_B1.Enums;
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

namespace AnhQuoc_WPF_C1_B1.UserControls
{
    /// <summary>
    /// Interaction logic for ucDisplaySeats.xaml
    /// </summary>
    public partial class ucDisplaySeats : UserControl
    {
        public ObservableCollection<ObservableCollection<Seat>> Seats { get; set; } = new ObservableCollection<ObservableCollection<Seat>>();
        public ucDisplaySeats()
        {
            InitializeComponent();
            GenerateLayout();

            this.DataContext = this;
        }

        private void GenerateLayout()
        {
            // Clear out any old elements if resetting the board
            Seats.Clear();

            string[] rows = { "A", "B", "C", "D", "E", "F", "G", "H", "I" };

            // The visual layout consists of 14 actual seat columns + 4 aisle spacing columns = 18 total grid indices.
            int totalVisualColumns = 18;

            for (int r = 0; r < rows.Length; r++)
            {
                var currentRow = new ObservableCollection<Seat>();

                for (int c = 0; c < totalVisualColumns; c++)
                {
                    // 1. Inject Pure Aisle Spacers (Visual columns: 2, 5, 10, 15)
                    if (c == 2 || c == 5 || c == 10 || c == 15)
                    {
                        currentRow.Add(null);
                        continue;
                    }

                    // 2. Map the absolute visual column index back to the natural seat number (1 to 14)
                    int naturalSeatNumber = GetNaturalSeatNumber(c);

                    // 3. Skip seats 13 and 14 for rows A through G (the isolated bottom-right section)
                    if (r < 7 && (naturalSeatNumber == 13 || naturalSeatNumber == 14))
                    {
                        currentRow.Add(null);
                        continue;
                    }

                    // 4. Remove seat A12 (Top-right structural/architectural corner gap)
                    if (r == 0 && naturalSeatNumber == 12)
                    {
                        currentRow.Add(null);
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

                    // 6. Append the verified seat node to our current structural row array
                    currentRow.Add(new Seat
                    {
                        Name = $"{rows[r]}{naturalSeatNumber}",
                        Type = sType
                    });
                }

                // Add the completed row matrix into our primary 2D collection
                Seats.Add(currentRow);
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
    }
}
