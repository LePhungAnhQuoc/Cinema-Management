using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_WPF_C1_B1
{
    public class CinemaTypeSchedule: INotifyPropertyChanged
    {
        private CinemaType _CinemaType;

        public CinemaType CinemaType
        {
            get { return _CinemaType; }
            set 
            { 
                _CinemaType = value;
                OnPropertyChanged();
            }
        }

        private List<CinemaSchedule> _CinemaSchedules;

        public List<CinemaSchedule> CinemaSchedules
        {
            get { return _CinemaSchedules; }
            set 
            { 
                _CinemaSchedules = value;
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

        public CinemaTypeSchedule()
        {
            CinemaSchedules = new List<CinemaSchedule>();
        }
    }
}
