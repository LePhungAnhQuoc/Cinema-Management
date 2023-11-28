using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_WPF_C1_B1
{
    public class MovieSchedule
    {
        public Movie Movie { get; set; }
        public List<CinemaTypeSchedule> CinemaTypeSchedules { get; set; }

        public MovieSchedule()
        {
            Movie = new Movie();
            CinemaTypeSchedules = new List<CinemaTypeSchedule>();
        }
    }
}
