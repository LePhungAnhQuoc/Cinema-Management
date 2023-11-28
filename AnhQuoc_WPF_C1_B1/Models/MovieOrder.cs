using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_WPF_C1_B1
{
    public class MovieOrder
    {
        public Movie Movie { get; set; }
        public CinemaType CinemaType { get; set; }
        public Cinema Cinema { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }

        public MovieOrder()
        {
            Movie = new Movie();
            Cinema = new Cinema();
        }
    }
}
