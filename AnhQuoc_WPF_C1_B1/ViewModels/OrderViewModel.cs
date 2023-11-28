using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml;

namespace AnhQuoc_WPF_C1_B1
{
    public class OrderViewModel
    {
        public RepositoryBase<Order> OrderRepo { get; set; }
        
        public Order Item { get; set; }

        public OrderViewModel()
        {
            Item = new Order();
        }

        public void getList(RepositoryBase<Order> OrderRepo)
        {
            this.OrderRepo = OrderRepo;
        }

        
        public RepositoryBase<Order> FillByDate(DateTime dateValue)
        {
            RepositoryBase<Order> result = new RepositoryBase<Order>();
            foreach (Order order in OrderRepo.Gets())
            {
                if (dateValue.Date == order.Date.Date)
                {
                    result.Add(order);
                }
            }
            return result;
        }
        
        public string GetId(int no)
        {
            return no.ToString();
        }

        public double TotalRevenue()
        {
            double total = 0.0;
            foreach (Order order in OrderRepo.Gets())
            {
                total += order.PayMent;
            }
            return total;
        }

        public void Write(XmlNode parentNode, List<Order> orders, bool isOpen = true)
        {
            if (!isOpen)
            {
                DataProvider.Instance.pathData = Constants.fOrders;
                DataProvider.Instance.Open();
                parentNode = DataProvider.Instance.nodeRoot;
            }
            foreach (Order order in orders)
                parentNode.AppendChild(Write(order));
            if (!isOpen)
                DataProvider.Instance.Close();
        }

        public XmlNode Write(Order order)
        {
            XmlNode newNode = DataProvider.Instance.createNode("Order");

            XmlAttribute newAttr = DataProvider.Instance.createAttr("Id");
            newAttr.Value = order.Id;
            newNode.Attributes.Append(newAttr);

            newAttr = DataProvider.Instance.createAttr("CustomerId");
            newAttr.Value = order.Customer.Id;
            newNode.Attributes.Append(newAttr);

            // Movie Order
            newAttr = DataProvider.Instance.createAttr("Cinema");
            newAttr.Value = order.MovieOrder.Cinema.Id.ToString();
            newNode.Attributes.Append(newAttr);

            newAttr = DataProvider.Instance.createAttr("OtherPrice");
            newAttr.Value = order.OtherPrice.ToString();
            newNode.Attributes.Append(newAttr);

            newAttr = DataProvider.Instance.createAttr("PayMent");
            newAttr.Value = order.PayMent.ToString();
            newNode.Attributes.Append(newAttr);

            newAttr = DataProvider.Instance.createAttr("Date");
            newAttr.Value = order.Date.ToShortDateString();
            newNode.Attributes.Append(newAttr);

            return newNode;
        }
    }
}
