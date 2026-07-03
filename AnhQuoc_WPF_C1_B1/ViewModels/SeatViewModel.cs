using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

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

                Type seatType = seat.GetType();

                foreach (XmlAttribute attribute in seatNode.Attributes)
                {
                    // Find a property on the Seat object that matches the XML attribute name
                    PropertyInfo property = seatType.GetProperty(attribute.Name, BindingFlags.Public | BindingFlags.Instance);

                    // If a matching writable property is found, assign the value
                    if (property != null && property.CanWrite)
                    {
                        try
                        {
                            // Handle Nullable types smoothly (e.g., int?, double?)
                            Type targetType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                            // Handle Enums specifically if your SeatType uses one
                            object convertedValue;
                            if (targetType.IsEnum)
                            {
                                convertedValue = Enum.Parse(targetType, attribute.Value);
                            }
                            else
                            {
                                // Safely convert the string value to the property's primitive type (int, double, bool, etc.)
                                convertedValue = Convert.ChangeType(attribute.Value, targetType);
                            }

                            // Assign the value to the object
                            property.SetValue(seat, convertedValue);
                        }
                        catch (Exception ex)
                        {
                            // Debugging tip: Catch conversion errors if XAML/XML values don't align with model types
                            System.Diagnostics.Debug.WriteLine($"Failed to map attribute {attribute.Name}: {ex.Message}");
                        }
                    }
                }


                try
                {
                    int nIsBooked = Convert.ToInt32(seatNode.Attributes["IsBooked"].Value);
                    //seat.IsBooked = Convert.ToBoolean(nIsBooked);
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
            DataProvider.Instance.pathData = fileName;
            DataProvider.Instance.Open();

            XmlNode root = DataProvider.Instance.nodeRoot;
            foreach (List<Seat> seatRow in lstSeats)
            {
                foreach (Seat seat in seatRow)
                {
                    XmlNode newNode = DataProvider.Instance.createNode("Seat");

                    // Get all public instance properties of the seat object
                    PropertyInfo[] properties = seat.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

                    foreach (PropertyInfo property in properties)
                    {
                        // Skip properties that cannot be read (just in case)
                        if (!property.CanRead) continue;

                        // Get the current value of the property from your seat object
                        object value = property.GetValue(seat);

                        // Only serialize if the value is not null
                        if (value != null)
                        {
                            // 1. Create the XML Attribute using its C# property name (e.g., "Id", "Price", "Type")
                            XmlAttribute newAttr = DataProvider.Instance.createAttr(property.Name);

                            // 2. Assign the string representation of the value
                            newAttr.Value = value.ToString();

                            // 3. Append it to the node's attributes collection
                            newNode.Attributes.Append(newAttr);
                        }
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
