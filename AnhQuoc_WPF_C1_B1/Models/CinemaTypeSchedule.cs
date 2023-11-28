using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_WPF_C1_B1
{
    public class CinemaTypeSchedule
    {
        public CinemaType CinemaType { get; set; }
        public List<CinemaSchedule> CinemaSchedules { get; set; }

        public CinemaTypeSchedule()
        {
            CinemaSchedules = new List<CinemaSchedule>();
        }
    }
}
