using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AnhQuoc_WPF_C1_B1
{
    public class CinemaTypeViewModel
    {
        public List<CinemaType> CinemaTypeRepo { get; set; }

        public CinemaTypeViewModel()
        {
            CinemaTypeRepo = new List<CinemaType>();
        }

        public void getList(List<CinemaType> CinemaTypeRepo)
        {
            this.CinemaTypeRepo = CinemaTypeRepo;
        }
        
        public string GetId(int no)
        {
            return no.ToString();
        }

        public List<CinemaType> FillByList(List<CinemaType> lst)
        {
            List<CinemaType> result = new List<CinemaType>();
            foreach (CinemaType parent in CinemaTypeRepo)
            {
                bool flag = true;
                foreach (CinemaType child in lst)
                {
                    if (parent == child)
                    {
                        flag = false;
                    }
                }
                if (flag == true)
                    result.Add(parent);
            }
            return result;
        }
    }
}
