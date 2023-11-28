using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_WPF_C1_B1
{
    public class GenreViewModel
    {
        public RepositoryBase<Genre> GenreRepo { get; set; }

        public GenreViewModel()
        {
            GenreRepo = new RepositoryBase<Genre>();
        }

        public void getList(RepositoryBase<Genre> GenreRepo)
        {
            this.GenreRepo = GenreRepo;
        }

        public Genre FindById(string idValue)
        {
            if (GenreRepo.Items == null)
                return null;
            foreach (Genre item in GenreRepo.Items)
            {
                if (idValue == item.Id)
                {
                    return item;
                }
            }
            return null;
        }

        public List<string> GetGenreIds(List<Genre> genres)
        {
            List<string> genreIds = new List<string>();
            foreach (Genre genre in genres)
            {
                genreIds.Add(genre.Id);
            }
            return genreIds;
        }
    }
}
