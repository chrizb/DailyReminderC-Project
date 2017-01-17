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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void reminder_Click(object sender, RoutedEventArgs e)
        {
            reminderside.Visibility = Visibility.Hidden;
            bookingsite.Visibility = Visibility.Visible;
        }

        private void oversikt_Click(object sender, RoutedEventArgs e)
        {
            oversiktside.Visibility = Visibility.Visible;
            reminderside.Visibility = Visibility.Hidden;
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            reminderside.Visibility = Visibility.Visible;
            oversiktside.Visibility = Visibility.Hidden;
            bookingsite.Visibility = Visibility.Hidden;
        }

        private void Colorchange_OnClick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            btn.Background = btn.Background == Brushes.Green ? (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF0000")): Brushes.Green;
        }

    }
}