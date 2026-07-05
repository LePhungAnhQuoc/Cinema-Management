using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AnhQuoc_WPF_C1_B1
{
    public class CinemaViewModel
    {
        public RepositoryBase<Cinema> CinemaRepo { get; set; }
        
        public CinemaViewModel()
        {
            CinemaRepo = new RepositoryBase<Cinema>();
        }

        public void getList(RepositoryBase<Cinema> CinemaRepo)
        {
            this.CinemaRepo = CinemaRepo;
        }

        public Cinema FindById(string idCinema)
        {
            foreach (Cinema cinema in CinemaRepo.Items)
            {
                if (idCinema == cinema.Id)
                {
                    return cinema;
                }
            }
            return null;
        }

        public List<Cinema> FillByType(CinemaType type)
        {
            List<Cinema> result = new List<Cinema>();
            foreach (Cinema cinema in CinemaRepo.Items)
            {
                if (type == cinema.Type)
                {
                    result.Add(cinema);
                }
            }
            return result;
        }

        /// <summary>
        /// Removes all elements from the source list that exist in the collection to exclude.
        /// </summary>
        public List<T> ExcludeItems<T>(List<T> source, IEnumerable<T> itemsToExclude)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (itemsToExclude == null) return source; // Nothing to remove

            // Convert to HashSet for fast O(1) lookups instead of O(N) linear scans
            var excludeSet = new HashSet<T>(itemsToExclude);

            // Remove in-place efficiently
            source.RemoveAll(item => excludeSet.Contains(item));
            return source;
        }
    }
}
