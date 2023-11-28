using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_WPF_C1_B1
{
    public class OrderDetail
    {
        public string Id { get; set; }
        public string IdOrder { get; set; }
        public Seat BookedSeat { get; set; }
        public TicketType TicketType { get; set; }

        public PayMent PayMent { get; set; }

        public OrderDetail()
        {
            PayMent = new PayMent();
            BookedSeat = new Seat();
        }
    }
}
