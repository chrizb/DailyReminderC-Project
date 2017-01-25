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
using dailyreminder.models;
using dailyreminder.controllers;

namespace dailyreminder {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        System.Windows.Forms.NotifyIcon icon = new System.Windows.Forms.NotifyIcon();

        // Controllers to be used:

        public MainController mainController;

        public MainWindow() {

            InitializeComponent();
            this.Closed += new EventHandler(MainWindow_Closed);
            this.Deactivated += new EventHandler(MainWindow_Deactivated);
            this.icon.Visible = true;
            this.icon.Icon = dailyreminder.Resources.Resource1.DailyReminderIcon;
            this.icon.ContextMenu = new System.Windows.Forms.ContextMenu();
            this.icon.ContextMenu.MenuItems.Add("dailyreminder app");
            this.icon.ContextMenu.MenuItems[0].Click += new EventHandler(icon_DoubleClick);
            this.icon.DoubleClick += new EventHandler(icon_DoubleClick);
            // Show login-popup
            mainController = new MainController(false);
            //mainController.initializeDataAndLogin();
        }

        

        BitmapImage blueButt = new BitmapImage(new Uri("Images/Buttons/blueButt.png", UriKind.Relative));
        BitmapImage blueHoverButt = new BitmapImage(new Uri("Images/Buttons/blueHoverButt.png", UriKind.Relative));
        BitmapImage blueClickedButt = new BitmapImage(new Uri("Images/Buttons/blueClickedButt.png", UriKind.Relative));

        BitmapImage greenButt = new BitmapImage(new Uri("Images/Buttons/addnewButt.png", UriKind.Relative));
        BitmapImage greenHoverButt = new BitmapImage(new Uri("Images/Buttons/addnewHoverButt.png", UriKind.Relative));
        BitmapImage greenClickedButt = new BitmapImage(new Uri("Images/Buttons/addnewClickedButt.png", UriKind.Relative));

        BitmapImage createreminderButt = new BitmapImage(new Uri("Images/Buttons/createreminderButt.png", UriKind.Relative));
        BitmapImage createreminderHoverButt = new BitmapImage(new Uri("Images/Buttons/createreminderHoverButt.png", UriKind.Relative));
        BitmapImage createreminderClickedButt = new BitmapImage(new Uri("Images/Buttons/createreminderClickedButt.png", UriKind.Relative));

        #region Mouseevents for Menu Buttons

        

        //---------------------------------------------------------------------------------------------------
        //----- Bookingsite
        private void addButt_MouseEnter(object sender, MouseEventArgs e) {
            if (bookingSite.Visibility != Visibility.Visible)
                addButt.Source = greenHoverButt;
        }

        private void addButt_MouseLeave(object sender, MouseEventArgs e) {
            if (bookingSite.Visibility != Visibility.Visible)
                addButt.Source = greenButt;
        }
        private void addButt_MouseDown(object sender, MouseButtonEventArgs e) {
            bookingSite.Visibility = Visibility.Visible;
            frontPage.Visibility = Visibility.Hidden;
            overView.Visibility = Visibility.Hidden;

            frontpageButt.Source = blueButt;
            overviewButt.Source = blueButt;
            addButt.Source = greenClickedButt;
        }
        //---------------------------------------------------------------------------------------------------
        //----- Frontpage 
        private void frontpageButt_MouseEnter(object sender, MouseEventArgs e) {
            if (frontPage.Visibility != Visibility.Visible)
                frontpageButt.Source = blueHoverButt;
        }
        private void frontpageButt_MouseLeave(object sender, MouseEventArgs e) {
            if (frontPage.Visibility != Visibility.Visible)
                frontpageButt.Source = blueButt;
        }
        private void frontpageButt_MouseDown(object sender, MouseButtonEventArgs e) {
            frontPage.Visibility = Visibility.Visible;
            overView.Visibility = Visibility.Hidden;
            bookingSite.Visibility = Visibility.Hidden;

            frontpageButt.Source = blueClickedButt;
            addButt.Source = greenButt;
            overviewButt.Source = blueButt;

            

        }
        //---------------------------------------------------------------------------------------------------
        //----- Overview
        private void overviewButt_MouseEnter(object sender, MouseEventArgs e) {
            if (overView.Visibility != Visibility.Visible)
                overviewButt.Source = blueHoverButt;
        }

        private void overviewButt_MouseLeave(object sender, MouseEventArgs e) {
            if (overView.Visibility != Visibility.Visible)
                overviewButt.Source = blueButt;
        }
        private void overviewButt_MouseDown(object sender, MouseButtonEventArgs e) {
            overView.Visibility = Visibility.Visible;
            frontPage.Visibility = Visibility.Hidden;
            bookingSite.Visibility = Visibility.Hidden;

            frontpageButt.Source = blueButt;
            addButt.Source = greenButt;
            overviewButt.Source = blueClickedButt;
        }



        #endregion

        private void createButt_MouseDown(object sender, MouseButtonEventArgs e) {
            createButt.Source = createreminderClickedButt;
            this.icon.ShowBalloonTip(10000, "Added Reminder", "AlarmTime", System.Windows.Forms.ToolTipIcon.Info);
            //Save reminder to the list/database!!
            Reminder newReminder = new Reminder();
            newReminder.Title = title.Text;
            newReminder.Description = description.Text;
            newReminder.startTime = Int32.Parse(startTime.Text);
            newReminder.endTime = Int32.Parse(stopTime.Text);
            newReminder.Days = getSelectedDays();
            mainController.addReminderToList(newReminder);
            
        }

        private void createButt_MouseEnter(object sender, MouseEventArgs e) {
            createButt.Source = createreminderHoverButt;
        }

        private void createButt_MouseLeave(object sender, MouseEventArgs e) {
            createButt.Source = createreminderButt;
        }

        void MainWindow_Closed(object sender, EventArgs e)
        {
            icon.Dispose();
            
        }

        void MainWindow_Deactivated(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
            {
                this.Hide();
            }
        }

        void icon_DoubleClick(object sender, EventArgs e)
        {
              this.Show();
              this.WindowState = WindowState.Normal;

        }


        private void ReminderBox_Loaded(object sender, RoutedEventArgs e)
        {}




        private String getSelectedDays() {
            string days = "";


            // Be prepared for nice code
            if (day_Monday.isToggled == true)
                days += "1";
            else
                days += "0";

            if (day_Tuesday.isToggled == true)
                days += "1";
            else
                days += "0";

            if (day_Wednesday.isToggled == true)
                days += "1";
            else
                days += "0";

            if (day_Thursday.isToggled == true)
                days += "1";
            else
                days += "0";

            if (day_Friday.isToggled == true)
                days += "1";
            else
                days += "0";

            if (day_Saturday.isToggled == true)
                days += "1";
            else
                days += "0";

            if (day_Sunday.isToggled == true)
                days += "1";
            else
                days += "0";


            return days;
        }

    }
}