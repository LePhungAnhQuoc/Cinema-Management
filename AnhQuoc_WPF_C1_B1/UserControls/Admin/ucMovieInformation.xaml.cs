using AnhQuoc_WPF_C1_B1.Views;
using Syncfusion.Windows.Controls;
using Syncfusion.Windows.Controls.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
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
using System.Windows.Threading;

namespace AnhQuoc_WPF_C1_B1.UserControls.Admin
{
    /// <summary>
    /// Interaction logic for ucMovieInformation.xaml
    /// </summary>
    public partial class ucMovieInformation : UserControl, INotifyPropertyChanged
    {
        #region Fields
        private Button addNewDateBtn;
        private Button addNewTimeBtn;
        private DispatcherTimer timer;
        private bool isUserDraggingSlider = false; // Kiểm tra xem người dùng có đang giữ kéo slider không
        private bool isPlaying = false;
        #endregion

        #region Dependency Property


        public RoleTypes GetRole
        {
            get { return (RoleTypes)GetValue(GetRoleProperty); }
            set { SetValue(GetRoleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GetRole.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GetRoleProperty =
            DependencyProperty.Register(nameof(GetRole), typeof(RoleTypes), typeof(ucMovieInformation), new PropertyMetadata(RoleTypes.Admin));


        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadonlyProperty); }
            set { SetValue(IsReadonlyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsReadonly.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsReadonlyProperty =
            DependencyProperty.Register(nameof(IsReadOnly), typeof(bool), typeof(ucMovieInformation), new PropertyMetadata(false));



        public Movie GetMovie
        {
            get { return (Movie)GetValue(GetMovieProperty); }
            set { SetValue(GetMovieProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GetMovie.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GetMovieProperty =
            DependencyProperty.Register(nameof(GetMovie), typeof(Movie), typeof(ucMovieInformation), new PropertyMetadata(null));


        #endregion


        #region Properties
        public ObservableCollection<CinemaTypeSchedule> CinemaTypeSchedulesCollection { get; set; }

        private CinemaTypeSchedule _CurrentItem;
        public CinemaTypeSchedule CurrentItem
        {
            get { return _CurrentItem; }
            set 
            { 
                _CurrentItem = value;
                OnPropertyChanged();
            }
        }

        private Movie _Movie;

        public Movie Movie
        {
            get { return _Movie; }
            set
            {
                _Movie = value;
                OnPropertyChanged();
            }
        }

        public string AllGenres { get; set; }

        public MovieSchedule MovieSchedule { get; set; }

        private string _MovieUrlTrailer;

        public string MovieUrlTrailer
        {
            get { return _MovieUrlTrailer; }
            set
            {
                _MovieUrlTrailer = value;
                OnPropertyChanged();
            }
        }

        private List<CinemaType> _CinemaTypes;
        public List<CinemaType> CinemaTypes
        {
            get { return _CinemaTypes; }
            set { 
                _CinemaTypes = value;
                OnPropertyChanged();
            }
        }

        private DateSchedule _SelectedDateSchedule;
        public DateSchedule SelectedDateSchedule
        {
            get { return _SelectedDateSchedule; }
            set 
            { 
                _SelectedDateSchedule = value;
                OnPropertyChanged();
            }
        }

        private CinemaSchedule _SelectedCinemaSchedule;
        public CinemaSchedule SelectedCinemaSchedule
        {
            get { return _SelectedCinemaSchedule; }
            set 
            {
                _SelectedCinemaSchedule = value;
                OnPropertyChanged();
            }
        }

        private TimeSchedule _SelectedTimeSchedule;

        public TimeSchedule SelectedTimeSchedule
        {
            get { return _SelectedTimeSchedule; }
            set 
            { 
                _SelectedTimeSchedule = value;
                OnPropertyChanged();
            }
        }


        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        public ucMovieInformation()
        {
            InitializeComponent();
            InitTimer();
            this.Loaded += FrmMovieInformation_Loaded;
        }

        private void FrmMovieInformation_Loaded(object sender, RoutedEventArgs e)
        {
            EnumViewModel enumViewModel = new EnumViewModel();
            CinemaTypes = enumViewModel.GetValues<CinemaType>().ToList();

            Movie = GetMovie;
            var names = Movie.Genres.Cast<Genre>()
                                      .Select(item => item.Name)
                                      .Where(name => !string.IsNullOrEmpty(name));
            AllGenres = string.Join(", ", names);


            MovieScheduleViewModel movieScheduleViewModel = new MovieScheduleViewModel();
            movieScheduleViewModel.MovieScheduleRepo = App.UnitOfWork.GetRepositoryMovieSchedule;

            MovieSchedule = movieScheduleViewModel.GetByMovie(Movie);
            if (MovieSchedule != null)
            {
                CinemaTypeSchedulesCollection = new ObservableCollection<CinemaTypeSchedule>(MovieSchedule.CinemaTypeSchedules);
            }
            else
            {
                CinemaTypeSchedulesCollection = new ObservableCollection<CinemaTypeSchedule>();

                MovieSchedule = new MovieSchedule();
                MovieSchedule.Movie = Movie;
                MovieSchedule.CinemaTypeSchedules = CinemaTypeSchedulesCollection.ToList();
            }
            if (Movie.UrlTrailer == string.Empty)
            {
                txtNoMovieTrailer.Visibility = Visibility.Visible;
                PlayerGrid.Visibility = Visibility.Collapsed;
            }
            else
            {
                MovieUrlTrailer = Utilities.GetVideoURL(Movie.UrlTrailer);
            }
            this.DataContext = this;
        }


        #region Video player methods
        private void InitTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(200); // Cập nhật mỗi 0.2 giây
            timer.Tick += Timer_Tick;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            // Nếu video đang chạy và người dùng KHÔNG kéo slider thì mới cập nhật slider theo video
            if (MyMediaElement.NaturalDuration.HasTimeSpan && !isUserDraggingSlider)
            {
                TimelineSlider.Value = MyMediaElement.Position.TotalSeconds;
                TxtCurrentTime.Text = MyMediaElement.Position.ToString(@"mm\:ss");
            }
        }
        // Khi video được tải lên thành công, thiết lập các thông số thời gian
        private void MyMediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            if (MyMediaElement.NaturalDuration.HasTimeSpan)
            {
                TimeSpan duration = MyMediaElement.NaturalDuration.TimeSpan;
                TimelineSlider.Maximum = duration.TotalSeconds;
                TxtTotalTime.Text = duration.ToString(@"mm\:ss");
            }
            timer.Start();
        }

        // Xử lý nút Play / Pause
        private void BtnPlayPause_Click(object sender, RoutedEventArgs e)
        {
            if (!isPlaying)
            {
                MyMediaElement.Play();
                BtnPlayPause.Content = "⏸"; // Đổi icon thành Pause
                isPlaying = true;
            }
            else
            {
                MyMediaElement.Pause();
                BtnPlayPause.Content = "▶"; // Đổi icon thành Play
                isPlaying = false;
            }
        }
        // Xử lý nút Stop
        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            MyMediaElement.Stop();
            BtnPlayPause.Content = "▶";
            isPlaying = false;
            TimelineSlider.Value = 0;
        }

        // Xử lý âm lượng
        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MyMediaElement.Volume = VolumeSlider.Value;
            if (BtnMute != null)
            {
                BtnMute.Content = VolumeSlider.Value == 0 ? "🔇" : "🔊";
            }
        }
        // Xử lý nút Mute nhanh âm thanh
        private void BtnMute_Click(object sender, RoutedEventArgs e)
        {
            if (MyMediaElement.Volume > 0)
            {
                MyMediaElement.Volume = 0;
                VolumeSlider.Value = 0;
                BtnMute.Content = "🔇";
            }
            else
            {
                MyMediaElement.Volume = 0.5;
                VolumeSlider.Value = 0.5;
                BtnMute.Content = "🔊";
            }
        }

        // Sự kiện xảy ra KHI ĐANG KÉO thanh tua (ValueChanged)
        private void TimelineSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (isUserDraggingSlider)
            {
                TxtCurrentTime.Text = TimeSpan.FromSeconds(TimelineSlider.Value).ToString(@"mm\:ss");
            }
        }
        // Bắt đầu nhấn giữ chuột vào Slider để tua
        private void TimelineSlider_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isUserDraggingSlider = true;
        }
        // Thả chuột ra -> Thiết lập vị trí mới cho Video
        private void TimelineSlider_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isUserDraggingSlider = false;
            MyMediaElement.Position = TimeSpan.FromSeconds(TimelineSlider.Value);
        }
        // Báo lỗi nếu đường dẫn video sai hoặc codec không hỗ trợ
        private void MyMediaElement_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            MessageBox.Show($"Video playback error: {e.ErrorException.Message}");
        }
        #endregion

        private void RatedDetail_Click(object sender, RoutedEventArgs e)
        {
            txtRatedDetail.Visibility = Visibility.Visible;
            txtRatedDetailHyperlink.Visibility = Visibility.Collapsed;
        }

        private void btnAddNewSchedule_Click(object sender, RoutedEventArgs e)
        {
            CinemaTypeSchedule newCinemaTypeSchedule = new CinemaTypeSchedule();
            newCinemaTypeSchedule.CinemaType = CinemaTypes[0];
            CinemaTypes.Remove(CinemaTypes[0]);

            CinemaTypeSchedulesCollection.Add(newCinemaTypeSchedule);

            if (CinemaTypes.Count == 0)
                btnAddNewSchedule.IsEnabled = false;
        }

        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            var expander = sender as Expander;
            // The DataContext of the Expander is the specific item in your collection
            var selectedSchedule = expander?.DataContext as CinemaTypeSchedule;

            if (selectedSchedule != null)
            {
                // Explicitly set your ViewModel's CurrentItem or update your logic
                CurrentItem = selectedSchedule;
            }
        }

        private void btnAddNewCinema_Click(object sender, RoutedEventArgs e)
        {
            // 1. Find the parent expander container
            DependencyObject obj = (DependencyObject)sender;
            while (obj != null && !(obj is Expander))
            {
                obj = VisualTreeHelper.GetParent(obj);
            }

            if (obj is Expander parentExpander && !parentExpander.IsExpanded)
            {
                // 2. Open it manually. This immediately executes the 'Expander_Expanded' method synchronously
                parentExpander.IsExpanded = true;
            }

            List<Cinema> cinemas = null;
            CinemaViewModel cinemaViewModel = new CinemaViewModel();
            cinemaViewModel.CinemaRepo = App.UnitOfWork.GetRepositoryCinema;

            cinemas = cinemaViewModel.FillByType(CurrentItem.CinemaType);

            // Get list of currently available movie theaters
            IEnumerable<Cinema> tempList = CurrentItem.CinemaSchedules.Select(item => item.Cinema);
            cinemas = cinemaViewModel.ExcludeItems(cinemas, tempList);

            if (cinemas.Count == 0)
            {
                MessageBox.Show("All the cinemas already have schedules.", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                frmSelectCinema frmSelectCinema = new frmSelectCinema();
                frmSelectCinema.GetCinemaSource = () => cinemas;
                frmSelectCinema.ShowDialog();

                if (frmSelectCinema.SelectedItems != null)
                {
                    foreach (var cinemaItem in frmSelectCinema.SelectedItems)
                    {
                        CinemaSchedule newCinemaSchedule = new CinemaSchedule();
                        newCinemaSchedule.Cinema = cinemaItem;

                        CurrentItem.CinemaSchedules.Add(newCinemaSchedule);
                    }
                    var temporaryList = CurrentItem.CinemaSchedules;
                    CurrentItem.CinemaSchedules = null; // Clear the binding reference
                    CurrentItem.CinemaSchedules = new List<CinemaSchedule>(temporaryList); // Re-assign
                }
            }    
        }

        private void btnAddNewDate_Click(object sender, RoutedEventArgs e)
        {
            addNewDateBtn = sender as Button;
            if (addNewDateBtn == null) 
                return;

            addNewDateBtn.Visibility = Visibility.Collapsed;

            // 2. Get the parent container (Grid, StackPanel, etc.)
            DependencyObject parent = VisualTreeHelper.GetParent(addNewDateBtn);

            // 3. Find the DatePicker among the parent's children
            if (parent != null)
            {
                int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
                for (int i = 0; i < childrenCount; i++)
                {
                    var child = VisualTreeHelper.GetChild(parent, i);
                    if (child is DatePicker datePicker)
                    {
                        // 4. Show the DatePicker (assuming its default was Collapsed or Hidden)
                        datePicker.Visibility = Visibility.Visible;

                        // Optional: Automatically open the dropdown calendar for the user
                        datePicker.IsDropDownOpen = true;
                        break;
                    }
                }
            }
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var datePicker = sender as DatePicker;
            datePicker.Visibility = Visibility.Collapsed;
            addNewDateBtn.Visibility = Visibility.Visible;

            if (addNewDateBtn.DataContext is CinemaSchedule cinemaSchedule)
            {
                DateSchedule newDateSchedule = new DateSchedule();
                if (datePicker.SelectedDate != null)
                {
                    newDateSchedule.Date = (DateTime)datePicker.SelectedDate;

                    cinemaSchedule.DatesSchedule.Add(newDateSchedule);

                    var temporaryList = cinemaSchedule.DatesSchedule;
                    cinemaSchedule.DatesSchedule = null; // Clear the binding reference
                    cinemaSchedule.DatesSchedule = new List<DateSchedule>(temporaryList); // Re-assign
                }
            }
        }

        private void btnAddNewTime_Click(object sender, RoutedEventArgs e)
        {
            addNewTimeBtn = sender as Button;
            if (addNewTimeBtn == null)
                return;

            addNewTimeBtn.Visibility = Visibility.Collapsed;

            var timePicker = Utilities.FindNeighboringControl<Xceed.Wpf.Toolkit.TimePicker>(addNewTimeBtn);
            timePicker.Visibility = Visibility.Visible;
        }

        private void btnTimeSchedule_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                if (button.DataContext is TimeSchedule timeSchedule)
                {
                    SelectedTimeSchedule = timeSchedule;
                }
            }

            frmBookingTicket frmBookingTicket = new frmBookingTicket();
            frmBookingTicket.Cinema = SelectedCinemaSchedule.Cinema;
            frmBookingTicket.Movie = GetMovie;
            frmBookingTicket.DateSchedule = SelectedDateSchedule;
            frmBookingTicket.TimeSchedule = SelectedTimeSchedule;
            //frmBookingTicket.SelectedSeats = 

            if (Application.Current.TryFindResource("BaseWindowStyle") is Style windowStyle)
            {
                frmBookingTicket.Style = windowStyle;
            }

            frmBookingTicket.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmBookingTicket.WindowState = WindowState.Maximized;
            frmBookingTicket.Show();
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            MovieSchedule.CinemaTypeSchedules = CinemaTypeSchedulesCollection.ToList();

            MovieScheduleViewModel movieScheduleViewModel = new MovieScheduleViewModel();
            movieScheduleViewModel.MovieScheduleRepo = App.UnitOfWork.GetRepositoryMovieSchedule;

            movieScheduleViewModel.WriteData(MovieSchedule);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void TimePicker_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (sender is Xceed.Wpf.Toolkit.TimePicker timePicker)
            {
                timePicker.Visibility = Visibility.Collapsed;
                addNewTimeBtn.Visibility = Visibility.Visible;

                // Your logic here:
                if (addNewTimeBtn.DataContext is DateSchedule dateSchedule)
                {
                    TimeSchedule newTimeSchedule = new TimeSchedule();
                    // Extracting your old and new times safely
                    DateTime? newTime = e.NewValue as DateTime?;
                    if (newTime.HasValue)
                    {
                        newTimeSchedule.Time = (TimeSpan)newTime?.TimeOfDay;

                        dateSchedule.TimeSchedules.Add(newTimeSchedule);

                        var temporaryList = dateSchedule.TimeSchedules;
                        dateSchedule.TimeSchedules = null; // Clear the binding reference
                        dateSchedule.TimeSchedules = new List<TimeSchedule>(temporaryList); // Re-assign
                    }
                }
            }
        }

        private void CinemaExpander_Expanded(object sender, RoutedEventArgs e)
        {
            if (sender is Expander expander)
            {
                if (expander.DataContext is CinemaSchedule cinemaSchedule)
                {
                    SelectedCinemaSchedule = cinemaSchedule;
                }
            }
        }

        private void DateSchedule_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListBox listBox)
            {
                if (listBox.SelectedItem is DateSchedule dateSchedule) {
                    SelectedDateSchedule = dateSchedule;
                }
            }
        }
    }
}

