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
                OnPropertyChanged();
            }
        }

        private string _Image;

        public string Image
        {
            get { return _Image; }
            set
            {
                _Image = value;
                OnPropertyChanged();
            }
        }

        private string _Username;

        public string Username
        {
            get { return _Username; }
            set
            {
                _Username = value;
                OnPropertyChanged();
            }
        }

        private string _Password;

        public string Password
        {
            get { return _Password; }
            set
            {
                _Password = value;
                OnPropertyChanged();
            }
        }


        private string _Email;
        public string Email
        {
            get { return _Email; }
            set
            {
                _Email = value;
                OnPropertyChanged();
            }
        }

        private string _Phone;

        public string Phone
        {
            get { return _Phone; }
            set
            {
                _Phone = value;
                OnPropertyChanged();
            }
        }

        private string _Address;
        public string Address
        {
            get { return _Address; }
            set
            {
                _Address = value;
                OnPropertyChanged();
            }
        }

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
