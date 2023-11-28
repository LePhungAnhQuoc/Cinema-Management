using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_WPF_C1_B1
{
    public class Movie
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Rated Rated { get; set; }
        public List<Genre> Genres { get; set; }
        public double RunningTime { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
        public string UrlImage { get; set; }
        public string UrlTrailer { get; set; }

        public Movie()
        {
            Rated = new Rated();
            Genres = new List<Genre>();
        }
    }
}
