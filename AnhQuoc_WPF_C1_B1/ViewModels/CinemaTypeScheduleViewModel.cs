using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AnhQuoc_WPF_C1_B1
{
    class CinemaTypeScheduleViewModel
    {
        public RepositoryBase<CinemaTypeSchedule> Repo { get; set; }

        public CinemaTypeScheduleViewModel()
        {
            Repo = new RepositoryBase<CinemaTypeSchedule>();
        }

        public List<CinemaType> FillCinemaType()
        {
            List<CinemaType> result = new List<CinemaType>();
            foreach (CinemaTypeSchedule item in Repo.Gets())
            {
                result.Add(item.CinemaType);
            }
            return result;
        }

        public CinemaTypeSchedule GetByCinemaType(CinemaType cinemaType)
        {
            foreach (CinemaTypeSchedule typeSchedule in Repo.Gets())
            {
                if (cinemaType == typeSchedule.CinemaType)
                    return typeSchedule;
            }
            return null;
        }

        public void WriteData(Movie movie, CinemaTypeSchedule item)
        {
            DataProvider.Instance.pathData = Constants.fMovieSchedules;
            DataProvider.Instance.Open();

            XmlNode movieNode = DataProvider.Instance.nodeRoot.SelectSingleNode($"MovieSchedule[@Movie='{movie.Id}']");
            XmlNode root = movieNode.SelectSingleNode("CinemaTypeSchedules");

            XmlNode newNode = DataProvider.Instance.createNode("CinemaTypeSchedule");
            XmlAttribute newAttr = DataProvider.Instance.createAttr("Type");
            newAttr.Value = item.CinemaType.ToString();
            newNode.Attributes.Append(newAttr);

            XmlNode childNode = DataProvider.Instance.createNode("CinemaSchedules");
            newNode.AppendChild(childNode);

            root.AppendChild(newNode);
            DataProvider.Instance.Close();
        }

        public void WriteRemoveData(Movie movie, CinemaTypeSchedule item)
        {
            DataProvider.Instance.pathData = Constants.fMovieSchedules;
            DataProvider.Instance.Open();

            XmlNode movieNode = DataProvider.Instance.nodeRoot.SelectSingleNode($"MovieSchedule[@Movie='{movie.Id}']");
            XmlNode root = movieNode.SelectSingleNode("CinemaTypeSchedules");

            XmlNode deleteNode = root.SelectSingleNode($"CinemaTypeSchedule[@Type='{item.CinemaType}']");
            root.RemoveChild(deleteNode);

            DataProvider.Instance.Close();
        }

        public string CreateFileSeatName(CinemaTypeSchedule cinemaTypeSchedule, string filePath)
        {
            filePath += "\\" + cinemaTypeSchedule.CinemaType.ToString();
            return filePath;
        }
    }
}
