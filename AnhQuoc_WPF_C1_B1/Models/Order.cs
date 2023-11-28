using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_WPF_C1_B1
{
    public class Order
    {
        public string Id { get; set; }
        public Customer Customer { get; set; }
        public DateTime Date { get; set; }

        public double OtherPrice { get; set; }

        public double PayMent { get; set; }

        public MovieOrder MovieOrder { get; set; }

        public List<OrderDetail> Details { get; set; }

        public Order()
        {
            Customer = new Customer();
            Details = new List<OrderDetail>();
            MovieOrder = new MovieOrder();
            OtherPrice = 0;
        }
    }
}
