using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_WPF_C1_B1
{
    public class CinemaSchedule
    {
        public Cinema Cinema { get; set; }
        public List<DateSchedule> DatesSchedule { get; set; }

        public CinemaSchedule()
        {
            Cinema = new Cinema();
            DatesSchedule = new List<DateSchedule>();
        }
    }
}
