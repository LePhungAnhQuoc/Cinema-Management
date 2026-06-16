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

namespace AnhQuoc_WPF_C1_B1.UserControls.Templates
{
    /// <summary>
    /// Interaction logic for ucDashboardCard.xaml
    /// </summary>
    public partial class ucDashboardCard : UserControl
    {


        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title), typeof(string), typeof(ucDashboardCard), new PropertyMetadata("Title"));



        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(int), typeof(ucDashboardCard), new PropertyMetadata(0));



        public Brush OuterBackground
        {
            get { return (Brush)GetValue(OuterBackgroundProperty); }
            set { SetValue(OuterBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OuterBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OuterBackgroundProperty =
            DependencyProperty.Register(nameof(OuterBackground), typeof(Brush), typeof(ucDashboardCard), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3FACFF"))));



        public Brush InnerBackground
        {
            get { return (Brush)GetValue(InnerBackgroundProperty); }
            set { SetValue(InnerBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InnerBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InnerBackgroundProperty =
            DependencyProperty.Register(nameof(InnerBackground), typeof(Brush), typeof(ucDashboardCard), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#68bcfc"))));





        public Geometry IconData
        {
            get { return (Geometry)GetValue(IconDataProperty); }
            set { SetValue(IconDataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IconData.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconDataProperty =
            DependencyProperty.Register(nameof(IconData), typeof(Geometry), typeof(ucDashboardCard), new PropertyMetadata(null));



        public ucDashboardCard()
        {
            InitializeComponent();
        }
    }
}
