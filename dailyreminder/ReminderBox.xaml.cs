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
    /// Interaction logic for ReminderBox.xaml
    /// </summary>
    public partial class ReminderBox : UserControl
    {
        public ReminderBox()
        {
            InitializeComponent();
            ReminderBoxTitle1.Content = "Buy Milk";
            timeCountdown1.Content = "1 Hours Left";
           
        }

        public Label ReminderBoxTitle1
        {
            get { return reminderBoxTitle1; }
            set { reminderBoxTitle1 = value; }
        }
    }
}
