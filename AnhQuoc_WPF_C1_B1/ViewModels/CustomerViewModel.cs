using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AnhQuoc_WPF_C1_B1
{
    public class CustomerViewModel
    {
        public RepositoryBase<Customer> CustomerRepo { get; set; }

        public void getList(RepositoryBase<Customer> CustomerRepo)
        {
            this.CustomerRepo = CustomerRepo;
        }
        
        public Customer FindById(string idCustomer)
        {
            foreach (Customer customer in CustomerRepo.Items)
            {
                if (idCustomer == customer.Id)
                {
                    return customer;
                }
            }
            return null;
        }

        public void Write(XmlNode parentNode, List<Customer> customers, bool isOpen = true)
        {
            if (!isOpen)
            {
                DataProvider.Instance.pathData = Constants.fCustomers;
                DataProvider.Instance.Open();
                parentNode = DataProvider.Instance.nodeRoot;
            }
            foreach (Customer customer in customers)
                parentNode.AppendChild(Write(customer));
            if (!isOpen)
                DataProvider.Instance.Close();
        }

        public XmlNode Write(Customer customer)
        {
            XmlNode newNode = DataProvider.Instance.createNode("Customer");

            XmlAttribute newAttr = DataProvider.Instance.createAttr("Id");
            newAttr.Value = customer.Id;
            newNode.Attributes.Append(newAttr);

            newAttr = DataProvider.Instance.createAttr("Name");
            newAttr.Value = customer.Name;
            newNode.Attributes.Append(newAttr);

            newAttr = DataProvider.Instance.createAttr("Phone");
            newAttr.Value = customer.Phone.ToString();
            newNode.Attributes.Append(newAttr);
            
            return newNode;
        }

        public string GetId(int no)
        {
            return no.ToString();
        }
    }
}
