using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AnhQuoc_WPF_C1_B1
{
    public class DateScheduleViewModel
    {
        public RepositoryBase<DateSchedule> DateScheduleRepo { get; set; }

        public DateScheduleViewModel()
        {
            DateScheduleRepo = new RepositoryBase<DateSchedule>();
        }

        public void getList(RepositoryBase<DateSchedule> DateScheduleRepo)
        {
            this.DateScheduleRepo = DateScheduleRepo;
        }

        public List<DateTime> FillDate()
        {
            List<DateTime> dates = new List<DateTime>();
            foreach (DateSchedule schedule in DateScheduleRepo.Gets())
            {
                dates.Add(schedule.Date);
            }
            return dates;
        }

        public DateSchedule GetByDate(DateTime date)
        {
            foreach (DateSchedule schedule in DateScheduleRepo.Gets())
            {
                if (date.Date == schedule.Date.Date)
                    return schedule;
            }
            return null;
        }

        public void WriteData(Movie movie, CinemaType cinemaType, Cinema cinema, DateSchedule item)
        {
            DataProvider.Instance.pathData = Constants.fMovieSchedules;
            DataProvider.Instance.Open();

            XmlNode movieNode = DataProvider.Instance.nodeRoot.SelectSingleNode($"MovieSchedule[@Movie='{movie.Id}']");
            XmlNode cinemaTypeNode = movieNode.FirstChild.SelectSingleNode($"CinemaTypeSchedule[@Type='{cinemaType}']");
            XmlNode cinemaNode = cinemaTypeNode.FirstChild.SelectSingleNode($"CinemaSchedule[@Cinema='{cinema.Id}']");

            XmlNode root = cinemaNode.SelectSingleNode("DateSchedules");
            XmlNode newNode = DataProvider.Instance.createNode("DateSchedule");
            XmlAttribute newAttr = DataProvider.Instance.createAttr("Date");
            newAttr.Value = item.Date.ToString(Constants.formatDate);
            newNode.Attributes.Append(newAttr);

            XmlNode childNode = DataProvider.Instance.createNode("TimeSchedules");
            newNode.AppendChild(childNode);

            root.AppendChild(newNode);
            DataProvider.Instance.Close();
        }

        public void WriteUpdateData(Movie movie, CinemaType cinemaType, Cinema cinema, DateTime oldItem, DateTime newItem)
        {
            DataProvider.Instance.pathData = Constants.fMovieSchedules;
            DataProvider.Instance.Open();

            XmlNode movieNode = DataProvider.Instance.nodeRoot.SelectSingleNode($"MovieSchedule[@Movie='{movie.Id}']");
            XmlNode cinemaTypeNode = movieNode.FirstChild.SelectSingleNode($"CinemaTypeSchedule[@Type='{cinemaType}']");
            XmlNode cinemaNode = cinemaTypeNode.FirstChild.SelectSingleNode($"CinemaSchedule[@Cinema='{cinema.Id}']");

            XmlNode root = cinemaNode.SelectSingleNode("DateSchedules");
            XmlNode updateNode =  root.SelectSingleNode($"DateSchedule[@Date='{oldItem.ToString(Constants.formatDate)}']");
            updateNode.Attributes["Date"].Value = newItem.ToString(Constants.formatDate);
            
            DataProvider.Instance.Close();
        }

        public void WriteRemoveData(Movie movie, CinemaType cinemaType, Cinema cinema, DateSchedule item)
        {
            DataProvider.Instance.pathData = Constants.fMovieSchedules;
            DataProvider.Instance.Open();

            XmlNode movieNode = DataProvider.Instance.nodeRoot.SelectSingleNode($"MovieSchedule[@Movie='{movie.Id}']");
            XmlNode cinemaTypeNode = movieNode.FirstChild.SelectSingleNode($"CinemaTypeSchedule[@Type='{cinemaType}']");
            XmlNode cinemaNode = cinemaTypeNode.FirstChild.SelectSingleNode($"CinemaSchedule[@Cinema='{cinema.Id}']");

            XmlNode root = cinemaNode.SelectSingleNode("DateSchedules");
            XmlNode removeNode = root.SelectSingleNode($"DateSchedule[@Date='{item.Date.ToString(Constants.formatDate)}']");
            root.RemoveChild(removeNode);
            
            DataProvider.Instance.Close();
        }


        public string CreateFileSeatName(DateTime date, string filePath)
        {
            filePath += "\\" + date.ToString(Constants.formatDateFile);
            return filePath;
        }
    }
}
