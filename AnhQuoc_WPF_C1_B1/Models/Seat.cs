using AnhQuoc_WPF_C1_B1.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_WPF_C1_B1
{
    public class Seat: INotifyPropertyChanged
    {
        public string Id { get; set; }
        public double Price { get; set; }

        private SeatType _type;
        public int RowIndex { get; set; }
        public int ColumnIndex { get; set; }

        public SeatType Type
        {
            get => _type;
            set { _type = value; OnPropertyChanged(); }
        }

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        #endregion
    }
}
