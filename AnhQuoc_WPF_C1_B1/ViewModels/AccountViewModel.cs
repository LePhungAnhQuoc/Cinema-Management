using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AnhQuoc_WPF_C1_B1
{
    public class AccountViewModel
    {
        public RepositoryBase<Account> AccountRepo { get; set; }

        public AccountViewModel()
        {
            AccountRepo = new RepositoryBase<Account>();
        }
        public void getList(RepositoryBase<Account> AccountRepo)
        {
            this.AccountRepo = AccountRepo;
        }
        public Account Find(Account value, int status)
        {
            foreach (Account item in AccountRepo.Items)
            {
                if (status != -1 && status == item.Status)
                {
                    if (value.Username == item.Username)
                    {
                        if (value.Password == item.Password)
                        {
                            return item;
                        }
                    }
                }
            }
            return null; // Not found account
        }

        public void WriteRemoveData(Account item)
        {
            DataProvider.Instance.pathData = Constants.fAccounts;
            DataProvider.Instance.Open();

            XmlNode root = DataProvider.Instance.nodeRoot;
            XmlNode removeNode = root.SelectSingleNode($"Account[@Username='{item.Username}']");
            root.RemoveChild(removeNode);

            DataProvider.Instance.Close();
        }

        public void WriteData(Account item)
        {
            DataProvider.Instance.pathData = Constants.fAccounts;
            DataProvider.Instance.Open();
            XmlAttribute newAttr = null;

            XmlNode root = DataProvider.Instance.nodeRoot;
            XmlNode newNode = DataProvider.Instance.createNode("Account");

            newAttr = DataProvider.Instance.createAttr("Username");
            newAttr.Value = item.Username;
            newNode.Attributes.Append(newAttr);

            newAttr = DataProvider.Instance.createAttr("Password");
            newAttr.Value = item.Password;
            newNode.Attributes.Append(newAttr);

            newAttr = DataProvider.Instance.createAttr("Role");
            newAttr.Value = item.Role.ToString();
            newNode.Attributes.Append(newAttr);

            newAttr = DataProvider.Instance.createAttr("Status");
            newAttr.Value = item.Status.ToString();
            newNode.Attributes.Append(newAttr);
            
            root.AppendChild(newNode);
            DataProvider.Instance.Close();
        }

        public void WriteUpdateData(Account newItem)
        {
            DataProvider.Instance.pathData = Constants.fAccounts;
            DataProvider.Instance.Open();

            XmlNode root = DataProvider.Instance.nodeRoot;
            XmlNode updateNode = root.SelectSingleNode($"Account[@Username='{newItem.Username}']");

            updateNode.Attributes["Password"].Value = newItem.Password;
            updateNode.Attributes["Role"].Value = newItem.Role.ToString();
            updateNode.Attributes["Status"].Value = newItem.Status.ToString();
            DataProvider.Instance.Close();
        }
    }
}
