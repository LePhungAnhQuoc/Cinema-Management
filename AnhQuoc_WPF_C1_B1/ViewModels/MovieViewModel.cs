using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AnhQuoc_WPF_C1_B1
{
    public class MovieViewModel
    {
        public RepositoryBase<Movie> MovieRepo { get; set; }

        public MovieViewModel()
        {
            MovieRepo = new RepositoryBase<Movie>();
        }

        public void getList(RepositoryBase<Movie> MovieRepo)
        {
            this.MovieRepo = MovieRepo;
        }
        
        public string GetId(int no)
        {
            return "MV" + no.ToString();
        }

        public void Write(XmlNode parentNode, List<Movie> movies, bool isOpen = true)
        {
            if (!isOpen)
            {
                DataProvider.Instance.pathData = Constants.fMovies;
                DataProvider.Instance.Open();
                parentNode = DataProvider.Instance.nodeRoot;
            }
            foreach (Movie movie in movies)
                parentNode.AppendChild(Write(movie));
            if (!isOpen)
                DataProvider.Instance.Close();
        }

        public XmlNode Write(Movie movie)
        {
            GenreViewModel genreVM = new GenreViewModel();
            XmlNode newNode = DataProvider.Instance.createNode("Movie");

            XmlAttribute newAttr = DataProvider.Instance.createAttr("Id");
            newAttr.Value = movie.Id;
            newNode.Attributes.Append(newAttr);

            newAttr = DataProvider.Instance.createAttr("Name");
            newAttr.Value = movie.Name;
            newNode.Attributes.Append(newAttr);

            newAttr = DataProvider.Instance.createAttr("Genres");
            newAttr.Value = Utilities.Join(genreVM.GetGenreIds(movie.Genres).ToArray());
            newNode.Attributes.Append(newAttr);

            newAttr = DataProvider.Instance.createAttr("ReleaseDate");
            newAttr.Value = movie.ReleaseDate.ToShortDateString();
            newNode.Attributes.Append(newAttr);

            newAttr = DataProvider.Instance.createAttr("RunningTime");
            newAttr.Value = movie.RunningTime.ToString();
            newNode.Attributes.Append(newAttr);

            newAttr = DataProvider.Instance.createAttr("Rated");
            newAttr.Value = movie.Rated.Id;
            newNode.Attributes.Append(newAttr);

            newAttr = DataProvider.Instance.createAttr("Description");
            newAttr.Value = movie.Description;
            newNode.Attributes.Append(newAttr);

            newAttr = DataProvider.Instance.createAttr("UrlImage");
            newAttr.Value = movie.UrlImage;
            newNode.Attributes.Append(newAttr);

            newAttr = DataProvider.Instance.createAttr("UrlTrailer");
            newAttr.Value = movie.UrlTrailer;
            newNode.Attributes.Append(newAttr);
            return newNode;
        }
        public XmlNode WriteDetail(Movie movie)
        {
            XmlNode newNode = DataProvider.Instance.createNode("MovieDetail");
            XmlAttribute newAttr = null;

            newAttr = DataProvider.Instance.createAttr("Id");
            newAttr.Value = movie.Id;
            newNode.Attributes.Append(newAttr);

            newAttr = DataProvider.Instance.createAttr("Description");
            newAttr.Value = movie.Description;
            newNode.Attributes.Append(newAttr);

            newAttr = DataProvider.Instance.createAttr("UrlImage");
            newAttr.Value = movie.UrlImage;
            newNode.Attributes.Append(newAttr);

            newAttr = DataProvider.Instance.createAttr("UrlTrailer");
            newAttr.Value = movie.UrlTrailer;
            newNode.Attributes.Append(newAttr);
            return newNode;
        }

        public Movie FindById(string idValue)
        {
            foreach (Movie item in MovieRepo.Gets())
            {
                if (idValue == item.Id)
                    return item;
            }
            return null;
        }

        public List<Movie> FillByList(List<Movie> lst)
        {
            List<Movie> result = new List<Movie>();
            foreach (Movie parent in MovieRepo.Gets())
            {
                bool flag = true;
                foreach (Movie child in lst)
                {
                    if (parent.Id == child.Id)
                    {
                        flag = false;
                    }
                }
                if (flag == true)
                    result.Add(parent);
            }
            return result;
        }
    }
}
