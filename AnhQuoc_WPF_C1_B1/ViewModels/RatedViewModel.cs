using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_WPF_C1_B1
{
    public class RatedViewModel
    {
        public RepositoryBase<Rated> RatedRepo { get; set; }

        public void getList(RepositoryBase<Rated> RatedRepo)
        {
            this.RatedRepo = RatedRepo;
        }
        public Rated FindById(string idValue)
        {
            if (RatedRepo.Items == null)
                return null;
            foreach (Rated item in RatedRepo.Items)
            {
                if (idValue == item.Id)
                {
                    return item;
                }
            }
            return null;
        }
    }
}
