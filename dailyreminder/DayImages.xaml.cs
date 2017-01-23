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

namespace dailyreminder
{
    /// <summary>
    /// Interaction logic for DayImages.xaml
    /// </summary>
    public partial class DayImages : UserControl
    {
        public DayImages()
        {
            InitializeComponent();
        }

        BitmapImage blueButt = new BitmapImage(new Uri("Images/Buttons/blueButt.png", UriKind.Relative));
        BitmapImage blueHoverButt = new BitmapImage(new Uri("Images/Buttons/blueHoverButt.png", UriKind.Relative));
        BitmapImage blueClickedButt = new BitmapImage(new Uri("Images/Buttons/blueClickedButt.png", UriKind.Relative));

        private void dayButt_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if (dayButt.Source != blueClickedButt)
                dayButt.Source = blueClickedButt;

            else dayButt.Source = new BitmapImage(new Uri("Images/frontpageButtPic.png", UriKind.Relative));
        }

        private void dayButt_MouseEnter(object sender, MouseEventArgs e)
        {
            if (dayButt.Source != blueClickedButt)
                dayButt.Source = blueHoverButt;
        }

        private void dayButt_MouseLeave(object sender, MouseEventArgs e)
        {
            if (dayButt.Source != blueClickedButt)
                dayButt.Source = blueButt;
        }
    }
}
