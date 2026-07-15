using AnhQuoc_WPF_C1_B1.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
                        bool isMatch = Argon2idHasher.VerifyPassword(value.PasswordHash, item.PasswordHash);

                        if (item.PasswordHash != null && isMatch)
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

            XmlNode root = DataProvider.Instance.nodeRoot;

            if (item == null)
                return;
            XmlNode newNode = DataProvider.Instance.createNode(nameof(Account));

            // Get all public instance properties of the seat object
            PropertyInfo[] properties = item.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo property in properties)
            {
                // Skip properties that cannot be read (just in case)
                if (!property.CanRead) continue;

                // Get the current value of the property from your seat object
                object value = property.GetValue(item);

                // Only serialize if the value is not null
                if (value != null)
                {
                    // 1. Create the XML Attribute using its C# property name (e.g., "Id", "Price", "Type")
                    XmlAttribute newAttr = DataProvider.Instance.createAttr(property.Name);

                    // 2. Assign the string representation of the value
                    newAttr.Value = value.ToString();

                    // 3. Append it to the node's attributes collection
                    newNode.Attributes.Append(newAttr);
                }
            }
            root.AppendChild(newNode);
            DataProvider.Instance.Close();
        }

        public void WriteUpdateData(Account newItem)
        {
            DataProvider.Instance.pathData = Constants.fAccounts;
            DataProvider.Instance.Open();

            XmlNode root = DataProvider.Instance.nodeRoot;
            XmlNode updateNode = root.SelectSingleNode($"Account[@Username='{newItem.Username}']");

            updateNode.Attributes["Password"].Value = newItem.PasswordHash;
            updateNode.Attributes["Role"].Value = newItem.Role.ToString();
            updateNode.Attributes["Status"].Value = newItem.Status.ToString();
            updateNode.Attributes["Image"].Value = newItem.Image;
            updateNode.Attributes["Email"].Value = newItem.Email;
            updateNode.Attributes["Phone"].Value = newItem.Phone;
            updateNode.Attributes["Address"].Value = newItem.Address;


            DataProvider.Instance.Close();
        }
    }
}
