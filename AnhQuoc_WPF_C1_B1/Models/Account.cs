using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_WPF_C1_B1
{
    public class Account: INotifyPropertyChanged
    {
        public RoleTypes Role { get; set; }

        private int _Status;
        public int Status
        {
            get { return _Status; }
            set
            {
                _Status = value;
                OnPropertyChanged("Status");
            }
        }
        public string Username { get; set; }
        public string Password { get; set; }

        public Account()
        {
            this.Username = string.Empty;
            this.Password = string.Empty;
        }

        public Account(string Username, string Password)
        {
            this.Username = Username;
            this.Password = Password;
        }

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
