using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AnhQuoc_WPF_C1_B1
{
    public class SeatViewModel
    {
        public RepositoryBase<List<Seat>> seatRepo { get; set; }

        public SeatViewModel()
        {
            seatRepo = new RepositoryBase<List<Seat>>();
        }
        
        public List<Seat> Merge2D(List<List<Seat>> seats2D)
        {
            List<Seat> result = new List<Seat>();

            foreach (List<Seat> seatRow in seats2D)
            {
                result.AddRange(seatRow);
            }
            return result;
        }

        public void GetListSeat(List<List<Seat>> lstSeats, XmlNodeList lstNode)
        {
            char letterRowPast = '0';
            List<Seat> seatRow = null;
            foreach (XmlNode seatNode in lstNode)
            {
                string idSeat = seatNode.Attributes["Id"].Value;
                char letterRow = idSeat[idSeat.Length - 1];

                if (seatRow == null || letterRow != letterRowPast)
                {
                    if (seatRow != null)
                    {
                        lstSeats.Add(seatRow);
                    }
                    seatRow = new List<Seat>();
                    letterRowPast = letterRow;
                }
                Seat seat = new Seat();
                seat.Id = idSeat;
                try
                {
                    int nIsBooked = Convert.ToInt32(seatNode.Attributes["IsBooked"].Value);
                    seat.IsBooked = Convert.ToBoolean(nIsBooked);
                }
                catch { }
                seatRow.Add(seat);
            }
            lstSeats.Add(seatRow);
        }

        public void SetSeatsPrice(List<List<Seat>> lstSeats, double priceCenter, double decreasePrice)
        {
            int middleRow = lstSeats.Count / 2;

            foreach (Seat middleSeat in lstSeats[middleRow])
            {
                middleSeat.Price = priceCenter;
            }

            double getPrice = priceCenter;
            for (int idxTop = middleRow - 1; idxTop >= 0; idxTop--)
            {
                getPrice -= decreasePrice;
                foreach (Seat middleSeat in lstSeats[idxTop])
                {
                    middleSeat.Price = getPrice;
                }
            }

            getPrice = priceCenter;
            for (int idxBottom = middleRow + 1; idxBottom < lstSeats.Count; idxBottom++)
            {
                getPrice -= decreasePrice;
                foreach (Seat middleSeat in lstSeats[idxBottom])
                {
                    middleSeat.Price = getPrice;
                }
            }
        }

        public void WriteListSeat(List<List<Seat>> lstSeats, string fileName)
        {
            XmlAttribute newAttr = null;
            DataProvider.Instance.pathData = fileName;
            DataProvider.Instance.Open();

            XmlNode root = DataProvider.Instance.nodeRoot;
            foreach (List<Seat> seatRow in lstSeats)
            {
                foreach (Seat seat in seatRow)
                {
                    XmlNode newNode = DataProvider.Instance.createNode("Seat");

                    newAttr = DataProvider.Instance.createAttr("Id");
                    newAttr.Value = seat.Id;
                    newNode.Attributes.Append(newAttr);
                    try
                    {
                        newAttr = DataProvider.Instance.createAttr("IsBooked");
                        newAttr.Value = Convert.ToInt32(seat.IsBooked).ToString();
                        newNode.Attributes.Append(newAttr);
                    }
                    catch
                    {
                        Utilities.HandleError();
                        return;
                    }
                    root.AppendChild(newNode);
                }
            }
            DataProvider.Instance.Close();
        }

        public Seat FindById(string idValue)
        {
            foreach (List<Seat> seatRow in seatRepo.Gets())
            {
                foreach (Seat seatItem in seatRow)
                {
                    if (idValue == seatItem.Id)
                        return seatItem;
                }
            }
            return null;
        }
    }
}
