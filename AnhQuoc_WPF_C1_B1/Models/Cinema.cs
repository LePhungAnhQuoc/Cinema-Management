using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_WPF_C1_B1
{
    public class Cinema
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public CinemaType Type { get; set; }
        public double PriceCenter { get; set; }
        public double DecreasePrice { get; set; }

		public Size Size { get; set; }
        public List<List<Seat>> Seats { get; set; }

        public Cinema()
        {
            Seats = new List<List<Seat>>();
            Size = new Size();
        }
    }
}
