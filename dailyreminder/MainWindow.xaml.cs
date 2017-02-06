﻿using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Threading;
using dailyreminder.models;
using dailyreminder.controllers;


namespace dailyreminder {
    public partial class MainWindow : Window {
        System.Windows.Forms.NotifyIcon icon = new System.Windows.Forms.NotifyIcon();

        
        // Controllers to be used:

        public MainController mainController;
        ListController listController;
        ListController overviewListController;
        AlarmController alarmController;

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
            //testin
                     
            // Show login-popup
            mainController = new MainController(false);
            mainController.initializeDataAndLogin();

            alarmController = new AlarmController(mainController.getTodaysReminders(), mainController);

        }

        BitmapImage blueButt = new BitmapImage(new Uri("Images/Buttons/blueTodayButt.png", UriKind.Relative));
        BitmapImage blueHoverButt = new BitmapImage(new Uri("Images/Buttons/blueHoverTodayButt.png", UriKind.Relative));

        BitmapImage blueOverviewButt = new BitmapImage(new Uri("Images/Buttons/blueOverviewButt.png", UriKind.Relative));
        BitmapImage blueHoverOverviewButt = new BitmapImage(new Uri("Images/Buttons/blueHoverOverviewButt.png", UriKind.Relative));

        BitmapImage greenButt = new BitmapImage(new Uri("Images/Buttons/addnewButt.png", UriKind.Relative));
        BitmapImage greenHoverButt = new BitmapImage(new Uri("Images/Buttons/addnewHoverButt.png", UriKind.Relative));

        BitmapImage createreminderButt = new BitmapImage(new Uri("Images/Buttons/createreminderButt.png", UriKind.Relative));
        BitmapImage createreminderHoverButt = new BitmapImage(new Uri("Images/Buttons/createreminderHoverButt.png", UriKind.Relative));

        BitmapImage deletereminderButt = new BitmapImage(new Uri("Images/Buttons/deleteReminderButt.png", UriKind.Relative));
        BitmapImage deletereminderHoverButt = new BitmapImage(new Uri("Images/Buttons/deleteReminderHoverButt.png", UriKind.Relative));

        BitmapImage rightArrowButt = new BitmapImage(new Uri("Images/rightArrow.png", UriKind.Relative));
        BitmapImage rightArrowHoverButt = new BitmapImage(new Uri("Images/rightArrowHover.png", UriKind.Relative));
        BitmapImage rightArrowClickedButt = new BitmapImage(new Uri("Images/rightArrowPressed.png", UriKind.Relative));

        String[] daysOfWeek = new String[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

        
        
        

        #region Mouseevents for Menu Buttons

        /*--------------------------------Add Reminder--------------------------------*/
        private void addButt_MouseEnter(object sender, MouseEventArgs e) {
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
            overViewNavbar.Visibility = Visibility.Hidden;

            overviewScroller.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            frontScroller.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;

            frontpageButt.Source = blueButt;
            overviewButt.Source = blueOverviewButt;
        }
        /*--------------------------------Frontpage--------------------------------*/
        private void frontpageButt_MouseEnter(object sender, MouseEventArgs e) {
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
            overViewNavbar.Visibility = Visibility.Hidden;

            overviewScroller.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            frontScroller.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;

            addButt.Source = greenButt;
            overviewButt.Source = blueOverviewButt;

            updateOverview();

        }
      
        /*--------------------------------Overview--------------------------------*/
        private void updateOverview()
        {
            // List the reminders
            try
            {
                listController.ResetGrid();
            }
            catch { }

            List<Reminder> reminders = mainController.getTodaysReminders();

            listController = new ListController(frontPage, reminders, mainController);
            listController.ListAllFrontPage();
        }

        private void overviewButt_MouseEnter(object sender, MouseEventArgs e) {
            overviewButt.Source = blueHoverOverviewButt;
        }

        private void overviewButt_MouseLeave(object sender, MouseEventArgs e) {
            if (overViewNavbar.Visibility != Visibility.Visible)
                overviewButt.Source = blueOverviewButt;
        }
        private void overviewButt_MouseDown(object sender, MouseButtonEventArgs e)
        {
            overView.Visibility = Visibility.Visible;
            overViewNavbar.Visibility = Visibility.Visible;
            frontPage.Visibility = Visibility.Hidden;
            bookingSite.Visibility = Visibility.Hidden;

            overviewScroller.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            frontScroller.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;

            frontpageButt.Source = blueButt;
            addButt.Source = greenButt;

            dayOfTheWeekLabel.Content = DateTime.Now.DayOfWeek;
            overviewListController = new ListController(overView, mainController.getADaysReminders(nameOfDayToNumber(dayOfTheWeekLabel.Content.ToString())), mainController);
            overviewListController.ListAllOverview();
        }

        private int nameOfDayToNumber(string str) {
            switch (str) {
                case "Monday":
                    return 1;
                case "Tuesday":
                    return 2;
                case "Wednesday":
                    return 3;
                case "Thursday":
                    return 4;
                case "Friday":
                    return 5;
                case "Saturday":
                    return 6;
                case "Sunday":
                    return 0;
            }
            return -1;
        }

        #endregion

        /************************** 
         * FUNCTION: Creates a reminder and adds it to a list when createButt is pressed
         **************************/
        private void createButt_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //createButt.Source = createreminderClickedButt;

            if (startTime.Text != "" && stopTime.Text != "" && title.Text != "" && getSelectedDays().Contains("1") == true)
            {
                // Gives the user a notification on the system tray 
                this.icon.ShowBalloonTip(10000, "Added Reminder", "AlarmTime", System.Windows.Forms.ToolTipIcon.Info);

                Reminder newReminder = new Reminder();
                newReminder.Title = title.Text;
                newReminder.Description = description.Text;
                newReminder.startTime = (startTime.Value.Value.Hour * 60) + startTime.Value.Value.Minute;
                newReminder.endTime = (stopTime.Value.Value.Hour * 60) + stopTime.Value.Value.Minute;
                newReminder.Days = getSelectedDays();

                mainController.addReminderToList(newReminder);
                if (newReminder.Days.ElementAt((int)DateTime.Now.DayOfWeek) == '1')
                { // Om larmet är idag
                    alarmController.addAlarm(newReminder);
                }

                //reset all windows in bookingsite
                ReminderClear();
            }
            else
            {
                if(startTime.Text == "" || stopTime.Text == "")
                popUpLabel.Content = "Choose Start and Alarm time";
                else if(title.Text == "")
                    popUpLabel.Content = "Choose a title";
                else popUpLabel.Content = "You must select atleast one day";
                popUp.Visibility = Visibility.Visible;
            }
          
        }

        private void createButt_MouseEnter(object sender, MouseEventArgs e)
        {
            createButt.Source = createreminderHoverButt;
        }

        private void createButt_MouseLeave(object sender, MouseEventArgs e)
        {
            createButt.Source = createreminderButt;
        }

        private void deleteButt_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Delete the reminder
        }

        private void deleteButt_MouseEnter(object sender, MouseEventArgs e)
        {
            deleteButt.Source = deletereminderHoverButt;
        }

        private void deleteButt_MouseLeave(object sender, MouseEventArgs e)
        {
            deleteButt.Source = deletereminderButt;
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

        /**************************
         * CALL: getSelectedDays() 
         * FUNCTION: gets the days that have been toggled via a  string
         * NOTE: 1 = toggled, 0 = untoggled
         **************************/
        private String getSelectedDays()
        {
            string days = "";

            #region if-else statements for checking toggled days

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

            #endregion

            return days;
        }

        #region MouseEvents for arrowButtons

        private void rightArrow_MouseDown(object sender, MouseButtonEventArgs e)
        {            
            for (int i = 0; i < daysOfWeek.Length; i++)
            {
                if (daysOfWeek[i] == dayOfTheWeekLabel.Content.ToString())
                {
                    dayOfTheWeekLabel.Content = daysOfWeek[(i + 1) % 7];
                    break;
                }
            }
            rightArrow.Source = rightArrowClickedButt;
            overviewListController.ResetGrid();
            overviewListController.setNewReminderList(mainController.getADaysReminders(nameOfDayToNumber(dayOfTheWeekLabel.Content.ToString())));
            overviewListController.ListAllOverview();

            // Ensures that the correct scrollviewer is displayed
            overviewScroller.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            frontScroller.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
        }
        private void rightArrow_MouseEnter(object sender, MouseEventArgs e)
        {
            rightArrow.Source = rightArrowHoverButt;
        }

        private void rightArrow_MouseLeave(object sender, MouseEventArgs e)
        {
            rightArrow.Source = rightArrowButt;
        }

        private void leftArrow_MouseEnter(object sender, MouseEventArgs e)
        {
            leftArrow.Source = rightArrowHoverButt;
        }

        private void leftArrow_MouseLeave(object sender, MouseEventArgs e)
        {
            leftArrow.Source = rightArrowButt;
        }

        private void leftArrow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            for (int i = 0; i < daysOfWeek.Length; i++)
            {
                if (daysOfWeek[i] == dayOfTheWeekLabel.Content.ToString())
                {
                    dayOfTheWeekLabel.Content = daysOfWeek[(i - 1 + 7) % 7];
                    break;
                }
            }

            leftArrow.Source = rightArrowClickedButt;
            overviewListController.ResetGrid();
            overviewListController.setNewReminderList(mainController.getADaysReminders(nameOfDayToNumber(dayOfTheWeekLabel.Content.ToString())));
            overviewListController.ListAllOverview();

            // Ensures that the correct scrollviewer is displayed
            overviewScroller.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            frontScroller.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
        }
        #endregion

        #region MouseEvents for dateTimeUpDown

        private void stopTime_KeyDown(object sender, KeyEventArgs e)
        {
            stopTime.IsReadOnly = true;
        }

        private void stopTime_MouseMove(object sender, MouseEventArgs e)
        {
            stopTime.IsReadOnly = false;
        }

        private void startTime_KeyDown(object sender, KeyEventArgs e)
        {
            startTime.IsReadOnly = true;
        }

        private void startTime_MouseMove(object sender, MouseEventArgs e)
        {
            startTime.IsReadOnly = false;
        }

#endregion

        private void popUpButt_Click(object sender, RoutedEventArgs e)
        {
            popUp.Visibility = Visibility.Hidden;
        }

        public void ReminderClear()
        {
            title.Text = "";
            startTime.Text = "";
            stopTime.Text = "";
        }

    }
}