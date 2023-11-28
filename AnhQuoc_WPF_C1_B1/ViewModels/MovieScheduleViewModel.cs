using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AnhQuoc_WPF_C1_B1
{
    public class MovieScheduleViewModel
    {
        public RepositoryBase<MovieSchedule> MovieScheduleRepo { get; set; }

        public MovieScheduleViewModel()
        {
            MovieScheduleRepo = new RepositoryBase<MovieSchedule>();
        }

        public void getList(RepositoryBase<MovieSchedule> MovieScheduleRepo)
        {
            this.MovieScheduleRepo = MovieScheduleRepo;
        }

        public List<Movie> FillMovie()
        {
            List<Movie> movies = new List<Movie>();
            foreach (MovieSchedule schedule in MovieScheduleRepo.Gets())
            {
                movies.Add(schedule.Movie);
            }
            return movies;
        }

        public MovieSchedule GetByMovie(Movie movie)
        {
            foreach (MovieSchedule schedule in MovieScheduleRepo.Gets())
            {
                if (movie.Id == schedule.Movie.Id)
                    return schedule;
            }
            return null;
        }

        public MovieSchedule GetByIdMovie(string idMovie)
        {
            foreach (MovieSchedule schedule in MovieScheduleRepo.Gets())
            {
                if (idMovie == schedule.Movie.Id)
                    return schedule;
            }
            return null;
        }

        public void WriteData(MovieSchedule item)
        {
            DataProvider.Instance.pathData = Constants.fMovieSchedules;
            DataProvider.Instance.Open();

            XmlNode root = DataProvider.Instance.nodeRoot; 
            XmlNode newNode = DataProvider.Instance.createNode("MovieSchedule");
            XmlAttribute newAttr = DataProvider.Instance.createAttr("Movie");
            newAttr.Value = item.Movie.Id;
            newNode.Attributes.Append(newAttr);

            XmlNode childNode = DataProvider.Instance.createNode("CinemaTypeSchedules");
            newNode.AppendChild(childNode);

            root.AppendChild(newNode);
            DataProvider.Instance.Close();
        }

        public void WriteUpdateData(Movie oldMovie, Movie newMovie)
        {
            DataProvider.Instance.pathData = Constants.fMovieSchedules;
            DataProvider.Instance.Open();

            XmlNode root = DataProvider.Instance.nodeRoot;

            XmlNode updateNode = root.SelectSingleNode($"MovieSchedule[@Movie='{oldMovie.Id}']");
            updateNode.Attributes["Movie"].Value = newMovie.Id;

            DataProvider.Instance.Close();
        }

        public void WriteRemoveData(MovieSchedule item)
        {
            DataProvider.Instance.pathData = Constants.fMovieSchedules;
            DataProvider.Instance.Open();

            XmlNode root = DataProvider.Instance.nodeRoot;

            XmlNode removeNode = root.SelectSingleNode($"MovieSchedule[@Movie='{item.Movie.Id}']");
            root.RemoveChild(removeNode);
            
            DataProvider.Instance.Close();
        }

        public string CreateFileSeatName(MovieSchedule movieSchedule, string filePath)
        {
            filePath += "\\" + movieSchedule.Movie.Id;
            return filePath;
        }
    }
}
