using AnhQuoc_WPF_C1_B1.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AnhQuoc_WPF_C1_B1.UserControls.Templates
{
    /// <summary>
    /// Interaction logic for ucNetflixStyleUI.xaml
    /// </summary>
    public partial class ucNetflixStyleUI : UserControl
    {
        #region GetDatas


        public frmCashier GetFrmCashier
        {
            get { return (frmCashier)GetValue(GetFrmCashierProperty); }
            set { SetValue(GetFrmCashierProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GetFrmCashier.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GetFrmCashierProperty =
            DependencyProperty.Register(nameof(GetFrmCashier), typeof(frmCashier), typeof(ucNetflixStyleUI), new PropertyMetadata(null));


        public IEnumerable<Movie> GetMovies
        {
            get { return (IEnumerable<Movie>)GetValue(GetMoviesProperty); }
            set { SetValue(GetMoviesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GetMovies.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GetMoviesProperty =
            DependencyProperty.Register(nameof(GetMovies), typeof(IEnumerable<Movie>), typeof(ucNetflixStyleUI), new PropertyMetadata(null));
        #endregion

        public ucNetflixStyleUI()
        {
            InitializeComponent();
        }
        
        private void ScrollLeft_Click(object sender, RoutedEventArgs e)
        {
            // 1. Calculate target offset by subtracting distance (e.g., 400 pixels)
            double targetOffset = MoviesScrollViewer.HorizontalOffset - 400;

            // 2. Prevent the scrollbar from attempting to scroll past the beginning (0)
            if (targetOffset < 0)
            {
                targetOffset = 0;
            }

            // 3. Setup the smooth glide animation
            DoubleAnimation scrollAnimation = new DoubleAnimation
            {
                From = MoviesScrollViewer.HorizontalOffset,
                To = targetOffset,
                Duration = TimeSpan.FromSeconds(0.4),
                DecelerationRatio = 0.3
            };

            // 4. Kick off the animation
            MoviesScrollViewer.BeginAnimation(ScrollViewerBehavior.HorizontalOffsetProperty, scrollAnimation);
        }
        private void ScrollRight_Click(object sender, RoutedEventArgs e)
        {
            // 1. Calculate how far to scroll (e.g., scroll by 400 pixels)
            double targetOffset = MoviesScrollViewer.HorizontalOffset + 400;

            // 2. Setup the animation
            DoubleAnimation scrollAnimation = new DoubleAnimation
            {
                From = MoviesScrollViewer.HorizontalOffset, // Start from where the scrollbar currently is
                To = targetOffset,
                Duration = TimeSpan.FromSeconds(0.4),
                DecelerationRatio = 0.3 // Gives it that smooth, slowing-down momentum feel
            };

            // 3. Kick off the animation using our new behavior
            MoviesScrollViewer.BeginAnimation(ScrollViewerBehavior.HorizontalOffsetProperty, scrollAnimation);
        }

        private void MoviesScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            // Check if the event is already handled
            if (!e.Handled)
            {
                // Mark it handled here to stop the ScrollViewer from eating it
                e.Handled = true;

                // Create a new MouseWheelEventArgs routed event to bubble up
                MouseWheelEventArgs eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta)
                {
                    RoutedEvent = UIElement.MouseWheelEvent,
                    Source = sender
                };

                // Raise the event on the parent control so the outer window/scrollviewer handles it
                var parent = ((Control)sender).Parent as UIElement;
                parent?.RaiseEvent(eventArg);
            }
        }

        private void MoviePoster_MouseDown(object sender, MouseButtonEventArgs e)
        {
            GetFrmCashier.MoviePoster_MouseDown(sender, e);
        }
    }
}
