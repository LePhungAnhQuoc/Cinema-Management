using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AnhQuoc_WPF_C1_B1
{
    class CinemaScheduleViewModel
    {
        public RepositoryBase<CinemaSchedule> CinemaScheduleRepo { get; set; }

        public CinemaScheduleViewModel()
        {
            CinemaScheduleRepo = new RepositoryBase<CinemaSchedule>();
        }

        public void getList(RepositoryBase<CinemaSchedule> CinemaScheduleRepo)
        {
            this.CinemaScheduleRepo = CinemaScheduleRepo;
        }

        public List<Cinema> FillCinema()
        {
            List<Cinema> cinemas = new List<Cinema>();
            foreach (CinemaSchedule schedule in CinemaScheduleRepo.Gets())
            {
                cinemas.Add(schedule.Cinema);
            }
            return cinemas;
        }

        public CinemaSchedule GetByCinema(Cinema cinema)
        {
            foreach (CinemaSchedule schedule in CinemaScheduleRepo.Gets())
            {
                if (cinema.Id == schedule.Cinema.Id)
                    return schedule;
            }
            return null;
        }

        public void WriteData(Movie movie, CinemaType cinemaType, CinemaSchedule item)
        {
            DataProvider.Instance.pathData = Constants.fMovieSchedules;
            DataProvider.Instance.Open();

            XmlNode movieNode = DataProvider.Instance.nodeRoot.SelectSingleNode($"MovieSchedule[@Movie='{movie.Id}']");
            XmlNode cinemaTypeNode = movieNode.FirstChild.SelectSingleNode($"CinemaTypeSchedule[@Type='{cinemaType}']");

            XmlNode root = cinemaTypeNode.SelectSingleNode("CinemaSchedules");
            XmlNode newNode = DataProvider.Instance.createNode("CinemaSchedule");
            XmlAttribute newAttr = DataProvider.Instance.createAttr("Cinema");
            newAttr.Value = item.Cinema.Id;
            newNode.Attributes.Append(newAttr);

            XmlNode childNode = DataProvider.Instance.createNode("DateSchedules");
            newNode.AppendChild(childNode);

            root.AppendChild(newNode);
            DataProvider.Instance.Close();
        }

        public void WriteUpdateData(Movie movie, CinemaType cinemaType, Cinema oldItem, Cinema newItem)
        {
            DataProvider.Instance.pathData = Constants.fMovieSchedules;
            DataProvider.Instance.Open();

            XmlNode movieNode = DataProvider.Instance.nodeRoot.SelectSingleNode($"MovieSchedule[@Movie='{movie.Id}']");
            XmlNode cinemaTypeNode = movieNode.FirstChild.SelectSingleNode($"CinemaTypeSchedule[@Type='{cinemaType}']");

            XmlNode root = cinemaTypeNode.SelectSingleNode("CinemaSchedules");

            XmlNode updateNode = root.SelectSingleNode($"CinemaSchedule[@Cinema='{oldItem.Id}']");
            updateNode.Attributes["Cinema"].Value = newItem.Id;
            
            DataProvider.Instance.Close();
        }

        public void WriteRemoveData(Movie movie, CinemaType cinemaType, CinemaSchedule item)
        {
            DataProvider.Instance.pathData = Constants.fMovieSchedules;
            DataProvider.Instance.Open();

            XmlNode movieNode = DataProvider.Instance.nodeRoot.SelectSingleNode($"MovieSchedule[@Movie='{movie.Id}']");
            XmlNode cinemaTypeNode = movieNode.FirstChild.SelectSingleNode($"CinemaTypeSchedule[@Type='{cinemaType}']");

            XmlNode root = cinemaTypeNode.SelectSingleNode("CinemaSchedules");
            XmlNode removeNode = root.SelectSingleNode($"CinemaSchedule[@Cinema='{item.Cinema.Id}']");
            root.RemoveChild(removeNode);
            
            DataProvider.Instance.Close();
        }

        public string CreateFileSeatName(CinemaSchedule cinemaSchedule, string filePath)
        {
            filePath += "\\" + cinemaSchedule.Cinema.Id;
            return filePath;
        }
    }
}
