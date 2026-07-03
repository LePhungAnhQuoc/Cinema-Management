using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_WPF_C1_B1
{
    public class Movie : INotifyPropertyChanged
    {
        private string _Id;

        public string Id
        {
            get { return _Id; }
            set { 
                _Id = value;
                OnPropertyChanged();
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private Rated _rated;
        public Rated Rated
        {
            get { return _rated; }
            set
            {
                _rated = value;
                OnPropertyChanged();
            }
        }

        private List<Genre> _genres;
        public List<Genre> Genres
        {
            get { return _genres; }
            set
            {
                _genres = value;
                OnPropertyChanged();
            }
        }

        private double _runningTime;
        public double RunningTime
        {
            get { return _runningTime; }
            set
            {
                _runningTime = value;
                OnPropertyChanged();
            }
        }

        private DateTime _releaseDate;
        public DateTime ReleaseDate
        {
            get { return _releaseDate; }
            set
            {
                _releaseDate = value;
                OnPropertyChanged();
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        private string _urlImage;
        public string UrlImage
        {
            get { return _urlImage; }
            set
            {
                _urlImage = value;
                OnPropertyChanged();
            }
        }

        private string _urlTrailer;
        public string UrlTrailer
        {
            get { return _urlTrailer; }
            set
            {
                _urlTrailer = value;
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

        public Movie()
        {
            Rated = new Rated();
            Genres = new List<Genre>();
        }
    }
}
