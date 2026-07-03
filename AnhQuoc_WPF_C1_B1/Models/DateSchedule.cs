using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_WPF_C1_B1
{
    public class DateSchedule: INotifyPropertyChanged
    {
        private DateTime _Date;
        public DateTime Date
        {
            get { return _Date; }
            set
            {
                _Date = value;
                OnPropertyChanged("Date");
            }
        }
        
        private List<TimeSchedule> _TimeShedules;
        public List<TimeSchedule> TimeShedules
        {
            get { return _TimeShedules; }
            set 
            { 
                _TimeShedules = value;
                OnPropertyChanged();
            }
        }


        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        public DateSchedule()
        {
            TimeShedules = new List<TimeSchedule>();
        }
    }
}
