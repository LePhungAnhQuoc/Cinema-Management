using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AnhQuoc_WPF_C1_B1
{
    public class Account: INotifyPropertyChanged
    {
        public Guid Id { get; set; }

        private string _FullName;
        public string FullName
        {
            get { return _FullName; }
            set 
            { 
                _FullName = value;
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

        private string _PasswordHash;
        public string PasswordHash
        {
            get { return _PasswordHash; }
            set
            {
                _PasswordHash = value;
                OnPropertyChanged();
            }
        }

        public RoleTypes Role { get; set; }

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

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Account()
        {
            this.Username = string.Empty;
            this.PasswordHash = string.Empty;
            this.Status = 1;
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
