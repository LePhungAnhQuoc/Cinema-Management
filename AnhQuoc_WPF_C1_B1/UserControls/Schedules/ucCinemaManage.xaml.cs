using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AnhQuoc_WPF_C1_B1
{
    /// <summary>
    /// Interaction logic for ucCinemaManage.xaml
    /// </summary>
    public partial class ucCinemaManage : UserControl
    {
        #region Properties
        public Func<string> getFileSeat { get; set; }
        public Func<MovieSchedule> getMovieSchedule { get; set; }
        public Func<frmAdmin> getFrmAdmin { get; set; }
        public Func<List<CinemaTypeSchedule>> getCinemaTypeSchedules { get; set; }

        public Func<List<CinemaType>> getAllCinemaTypes { get; set; }
        public Func<RepositoryBase<Cinema>> getCinemaRepo { get; set; }

        public ucCinemaScheduleTable ucCinemaSchedule;
        public ucCinemaTypeScheduleTable ucCinemaTypeTable;
        public ucDateScheduleTable ucDateSchedule;
        public ucTimeScheduleTable ucTimeSchedule;

        public Label lblCinemaInfo;
        #endregion
        public ucCinemaManage()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ucCinemaTypeTable = new ucCinemaTypeScheduleTable();
            ucCinemaTypeTable.Margin = new Thickness(0, 0, 0, 10);

            ucCinemaTypeTable.getFileSeat = getFileSeat;
            ucCinemaTypeTable.getMovieSchedule = getMovieSchedule;
            ucCinemaTypeTable.getUcCinemaManage = () => this;

            ucCinemaTypeTable.getCinemaTypeSchedule = getCinemaTypeSchedules;

            EnumViewModel enumVM = new EnumViewModel();
            ucCinemaTypeTable.getAllCinemaTypes = getAllCinemaTypes;
            ucCinemaTypeTable.getCinemaRepo = getCinemaRepo;

            Grid.SetRow(ucCinemaTypeTable, 0);
            gdCinema.Children.Add(ucCinemaTypeTable);

            ucCinemaSchedule = new ucCinemaScheduleTable();
            ucCinemaSchedule.Margin = new Thickness(0, 10, 0, 0);

            ucCinemaSchedule.getUcCinemaManage = () => this;
            ucCinemaSchedule.getCinemaSchedule = () => new List<CinemaSchedule>();

            Grid.SetRow(ucCinemaSchedule, 1);
            ucCinemaSchedule.IsEnabled = false;
            gdCinema.Children.Add(ucCinemaSchedule);

            StackPanel stackPanel = new StackPanel();
            stackPanel.Margin = new Thickness(0, 0, 0, 20);
            lblCinemaInfo = new Label();
            lblCinemaInfo.FontSize = 18;
            lblCinemaInfo.Content = "Cinema Choose: None";
            lblCinemaInfo.Margin = new Thickness(10);
            lblCinemaInfo.HorizontalAlignment = HorizontalAlignment.Center;
            stackPanel.Children.Add(lblCinemaInfo);

            ucDateSchedule = new ucDateScheduleTable();
            ucDateSchedule.getUcCinemaManage = () => this;
            ucDateSchedule.getDateSchedules = () => new List<DateSchedule>();
            stackPanel.Children.Add(ucDateSchedule);

            Grid.SetRow(stackPanel, 2);
            ucDateSchedule.IsEnabled = false;
            gdCinema.Children.Add(stackPanel);

            ucTimeSchedule = new ucTimeScheduleTable();
            ucTimeSchedule.getTimeSchedules = () => new List<TimeSchedule>();

            Grid.SetRow(ucTimeSchedule, 3);
            ucTimeSchedule.IsEnabled = false;
            gdCinema.Children.Add(ucTimeSchedule);
        }
    }
}
