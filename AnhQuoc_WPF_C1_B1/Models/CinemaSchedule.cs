using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_WPF_C1_B1
{
    public class CinemaSchedule: INotifyPropertyChanged
    {
        public Cinema Cinema { get; set; }

        private List<DateSchedule> _DatesSchedule;
        public List<DateSchedule> DatesSchedule
        {
            get { return _DatesSchedule; }
            set 
            { 
                _DatesSchedule = value;
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

        public CinemaSchedule()
        {
            Cinema = new Cinema();
            DatesSchedule = new List<DateSchedule>();
        }
    }
}
