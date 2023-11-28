using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AnhQuoc_WPF_C1_B1
{
    public class TimeScheduleViewModel
    {
        public RepositoryBase<TimeSchedule> TimeScheduleRepo { get; set; }

        public TimeScheduleViewModel()
        {
            TimeScheduleRepo = new RepositoryBase<TimeSchedule>();
        }
       
        public TimeSchedule Find(TimeSpan time, string format)
        {
            foreach (TimeSchedule item in TimeScheduleRepo.Gets())
            {
                if (item.Time.ToString(format) == time.ToString(format))
                {
                    return item;
                }
            }
            return null;
        }

        public void WriteData(Movie movie, CinemaType cinemaType, Cinema cinema, DateTime date, TimeSchedule item)
        {
            DataProvider.Instance.pathData = Constants.fMovieSchedules;
            DataProvider.Instance.Open();

            XmlNode movieNode = DataProvider.Instance.nodeRoot.SelectSingleNode($"MovieSchedule[@Movie='{movie.Id}']");
            XmlNode cinemaTypeNode = movieNode.FirstChild.SelectSingleNode($"CinemaTypeSchedule[@Type='{cinemaType}']");
            XmlNode cinemaNode = cinemaTypeNode.FirstChild.SelectSingleNode($"CinemaSchedule[@Cinema='{cinema.Id}']");
            XmlNode dateNode = cinemaNode.FirstChild.SelectSingleNode($"DateSchedule[@Date='{date.ToString(Constants.formatDate)}']");

            XmlNode root = dateNode.SelectSingleNode("TimeSchedules");
            XmlNode newNode = DataProvider.Instance.createNode("TimeSchedule");
            XmlAttribute newAttr = DataProvider.Instance.createAttr("Time");
            newAttr.Value = item.Time.ToString(Constants.formatTime);
            newNode.Attributes.Append(newAttr);
            
            root.AppendChild(newNode);
            DataProvider.Instance.Close();
        }

        public void WriteUpdateData(Movie movie, CinemaType cinemaType, Cinema cinema, DateTime date, TimeSpan oldItem, TimeSpan newItem)
        {
            DataProvider.Instance.pathData = Constants.fMovieSchedules;
            DataProvider.Instance.Open();

            XmlNode movieNode = DataProvider.Instance.nodeRoot.SelectSingleNode($"MovieSchedule[@Movie='{movie.Id}']");
            XmlNode cinemaTypeNode = movieNode.FirstChild.SelectSingleNode($"CinemaTypeSchedule[@Type='{cinemaType}']");
            XmlNode cinemaNode = cinemaTypeNode.FirstChild.SelectSingleNode($"CinemaSchedule[@Cinema='{cinema.Id}']");
            XmlNode dateNode = cinemaNode.FirstChild.SelectSingleNode($"DateSchedule[@Date='{date.ToString(Constants.formatDate)}']");

            XmlNode root = dateNode.SelectSingleNode("TimeSchedules");

            XmlNode updateNode = root.SelectSingleNode($"TimeSchedule[@Time='{oldItem.ToString(Constants.formatTime)}']");
            updateNode.Attributes["Time"].Value = newItem.ToString(Constants.formatTime);
            
            DataProvider.Instance.Close();
        }

        public void WriteRemoveData(Movie movie, CinemaType cinemaType, Cinema cinema, DateTime date, TimeSchedule item)
        {
            DataProvider.Instance.pathData = Constants.fMovieSchedules;
            DataProvider.Instance.Open();

            XmlNode movieNode = DataProvider.Instance.nodeRoot.SelectSingleNode($"MovieSchedule[@Movie='{movie.Id}']");
            XmlNode cinemaTypeNode = movieNode.FirstChild.SelectSingleNode($"CinemaTypeSchedule[@Type='{cinemaType}']");
            XmlNode cinemaNode = cinemaTypeNode.FirstChild.SelectSingleNode($"CinemaSchedule[@Cinema='{cinema.Id}']");
            XmlNode dateNode = cinemaNode.FirstChild.SelectSingleNode($"DateSchedule[@Date='{date.ToString(Constants.formatDate)}']");

            XmlNode root = dateNode.SelectSingleNode("TimeSchedules");
            XmlNode newNode = root.SelectSingleNode($"TimeSchedule[@Time='{item.Time.ToString(Constants.formatTime)}']");
            root.RemoveChild(newNode);
           
            DataProvider.Instance.Close();
        }
    
        public string CreateFileSeatName(TimeSpan time, string filePath)
        {
            filePath += "\\" + time.ToString(Constants.formatTimeFile) + ".xml";
            return filePath;
        }
    }
}
