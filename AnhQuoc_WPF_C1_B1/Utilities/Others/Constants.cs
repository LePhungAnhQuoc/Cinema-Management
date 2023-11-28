using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_WPF_C1_B1
{
    class Constants
    {
        // Main files
        public static string fAccounts = "Data/Accounts.xml";
        public static string fGenres = "Data/Genres.xml";
        public static string fRateds = "Data/Rateds.xml";
        public static string fMovies = "Data/Movies.xml";
        public static string fMovieSchedules = "Data/MovieSchedules.xml";
        public static string fMovieDetails = "Data/MovieDetails.xml";
        public static string fCinemas = "Data/Cinemas.xml";
        public static string fOrders = "Data/Orders.xml";
        public static string fOrderDetails = "Data/OrderDetails.xml";
        public static string fCustomers = "Data/Customers.xml";

        // Other files
        public static string fElectronics = "Data/General/Products/Electronics.xml";
        public static string fPorcelains = "Data/General/Products/Porcelains.xml";
        public static string fFoods = "Data/General/Products/Food.xml";

        public static string fNoImage = "Assets/no-image.png";


        // Unit
        public static string unitElectricPower = "kWh";
        public static string unitWarranty = "Months";
        public static string unitCurrency = "VND";

        // xml xpath
        public static string xpathProduct = "Product";

        // format
        public static string formatThousand = "#,##0.##";
        public static string formatWarranty = formatThousand + " " + unitWarranty;
        public static string formatElectricPower = formatThousand + " " + unitElectricPower;
        public static string formatDate = "dd/MM/yyyy";
        public static string formatDateFile = "dd-MM-yyyy";
        public static string formatDateTime = "dd/MM/yyyy HH:mm";
        public static string formatTime = "hh\\:mm";
        public static string formatTimeFile = "hh\\-mm";
        public static string formatCurrency = formatThousand + " " + unitCurrency;
        public static string formatDiscount = "0\\%";

        public static int posCenter = 20;
        public static int bufferLength = 150;
        public static string dateNone = "01/01/0001";
        public static string strEsc = "0";
    }
}
