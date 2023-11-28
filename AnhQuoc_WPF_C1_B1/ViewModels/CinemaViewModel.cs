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

        public List<Cinema> FillByList(List<Cinema> lst)
        {
            List<Cinema> result = new List<Cinema>();
            foreach (Cinema parent in CinemaRepo.Gets())
            {
                bool flag = true;
                foreach (Cinema child in lst)
                {
                    if (parent.Id == child.Id)
                    {
                        flag = false;
                    }
                }
                if (flag == true)
                {
                    result.Add(parent);
                }
            }
            return result;
        }
    }
}
