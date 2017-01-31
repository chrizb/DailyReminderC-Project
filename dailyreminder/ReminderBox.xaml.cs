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
using dailyreminder.controllers;
using dailyreminder.models;

namespace dailyreminder
{
    /// <summary>
    /// Interaction logic for ReminderBox.xaml
    /// </summary>
    public partial class ReminderBox : UserControl
    {


        

        MainWindow mainWindow;
        public ReminderBox()
        {
            InitializeComponent();

            mainWindow = (MainWindow)Application.Current.MainWindow;
            
            
            

        }

        public Label ReminderBoxTitle1
        {
            get { return reminderBoxTitle1; }
            set { reminderBoxTitle1 = value; }
        }

        

        public void listReminders() {
            List<Reminder> todaysReminders = mainWindow.mainController.getTodaysReminders();

            if (todaysReminders.Count != 0) {
                foreach (Reminder reminder in todaysReminders) {
                    ReminderBoxTitle1.Content = reminder.Title;
                    timeCountdown1.Content = reminder.Description;
                }

            }
        }

    }
}
