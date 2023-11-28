using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_WPF_C1_B1
{
    public class PayMent: INotifyPropertyChanged
    {
        private double _DiscountRef;
        public double DiscountRef
        {
            get { return _DiscountRef; }
            set
            {
                _DiscountRef = value;
                OnPropertyChanged("DiscountRef");
            }
        }
        private double _Discount;
        public double Discount
        {
            get { return _Discount; }
            set
            {
                _Discount = value;
                OnPropertyChanged("Discount");
            }
        }
        private double _Price;
        public double Price
        {
            get { return _Price; }
            set
            {
                _Price = value;
                OnPropertyChanged("Price");
            }
        }

        public PayMent()
        {
            Price = 0;
        }

        #region PropertyNotify
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
