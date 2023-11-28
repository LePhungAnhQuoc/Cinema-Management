using System.Collections.Generic;
using System.Xml;

namespace AnhQuoc_WPF_C1_B1
{
    public class OrderDetailViewModel
    {
        public RepositoryBase<OrderDetail> OrderDetailRepo { get; set; }

        public OrderDetail Item { get; set; }

        public OrderDetailViewModel()
        {
            Item = new OrderDetail();
        }

        public void getList(RepositoryBase<Order> OrderRepo)
        {
            this.OrderDetailRepo = OrderDetailRepo;
        }

        public string GetId(int no)
        {
            return no.ToString();
        }

        public void Write(XmlNode parentNode, List<OrderDetail> orderDetails, string idOrder)
        {
            XmlAttribute newAttr = DataProvider.Instance.createAttr("IdOrder");
            newAttr.Value = idOrder;
            parentNode.Attributes.Append(newAttr);
            foreach (OrderDetail detail in orderDetails)
            {
                parentNode.AppendChild(Write(detail));
            }
        }

        public XmlNode Write(OrderDetail orderDetail)
        {
            XmlNode newNode = DataProvider.Instance.createNode("OrderDetail");

            XmlAttribute newAttr = DataProvider.Instance.createAttr("Id");
            newAttr.Value = orderDetail.Id;
            newNode.Attributes.Append(newAttr);

            newAttr = DataProvider.Instance.createAttr("BookedSeatId");
            newAttr.Value = orderDetail.BookedSeat.Id;
            newNode.Attributes.Append(newAttr);

            newAttr = DataProvider.Instance.createAttr("TicketType");
            newAttr.Value = orderDetail.TicketType.ToString();
            newNode.Attributes.Append(newAttr);
            
            newAttr = DataProvider.Instance.createAttr("DiscountRef");
            newAttr.Value = orderDetail.PayMent.DiscountRef.ToString();
            newNode.Attributes.Append(newAttr);

            newAttr = DataProvider.Instance.createAttr("Discount");
            newAttr.Value = orderDetail.PayMent.Discount.ToString();
            newNode.Attributes.Append(newAttr);

            newAttr = DataProvider.Instance.createAttr("TotalPrice");
            newAttr.Value = orderDetail.PayMent.Price.ToString();
            newNode.Attributes.Append(newAttr);

            return newNode;
        }

        public OrderDetail FindBySeatId(string idSeat)
        {
            foreach (OrderDetail detail in OrderDetailRepo.Gets())
            {
                if (idSeat == detail.BookedSeat.Id)
                    return detail;
            }
            return null;
        }
    }
}
