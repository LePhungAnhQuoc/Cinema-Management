using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_WPF_C1_B1
{
    public class AccountInfo
    {
        public string Username { get; set; }

        public AccountInfo()
        {
            this.Username = string.Empty;
        }

        public AccountInfo(string Username, string Password)
        {
            this.Username = Username;
        }
    }
}
