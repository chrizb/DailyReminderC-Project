using System;
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


namespace dailyreminder
{
    public partial class MainWindow : Window
    {
        System.Windows.Forms.NotifyIcon icon = new System.Windows.Forms.NotifyIcon();

        // Controllers to be used:

        public MainController mainController;
        ListController listController;
        ListController overviewListController;
        AlarmController alarmController;

        public MainWindow()
        {

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

        BitmapImage dayButt = new BitmapImage(new Uri("Images/Buttons/blueButt.png", UriKind.Relative));
        BitmapImage dayHoverButt = new BitmapImage(new Uri("Images/Buttons/blueHoverButt.png", UriKind.Relative));
        BitmapImage dayClickedButt = new BitmapImage(new Uri("Images/Buttons/blueHoverButt.png", UriKind.Relative));

        String[] daysOfWeek = new String[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

        #region Mouseevents for Menu Buttons

        /*--------------------------------Add Reminder--------------------------------*/
        private void addButt_MouseEnter(object sender, MouseEventArgs e)
        {
            addButt.Source = greenHoverButt;
        }

        private void addButt_MouseLeave(object sender, MouseEventArgs e)
        {
            if (bookingSite.Visibility != Visibility.Visible)
                addButt.Source = greenButt;
        }
        private void addButt_MouseDown(object sender, MouseButtonEventArgs e)
        {
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
        private void frontpageButt_MouseEnter(object sender, MouseEventArgs e)
        {
            frontpageButt.Source = blueHoverButt;
        }
        private void frontpageButt_MouseLeave(object sender, MouseEventArgs e)
        {
            if (frontPage.Visibility != Visibility.Visible)
                frontpageButt.Source = blueButt;
        }
        private void frontpageButt_MouseDown(object sender, MouseButtonEventArgs e)
        {
            frontPage.Visibility = Visibility.Visible;
            overView.Visibility = Visibility.Hidden;
            bookingSite.Visibility = Visibility.Hidden;
            overViewNavbar.Visibility = Visibility.Hidden;

            overviewScroller.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            frontScroller.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;

            addButt.Source = greenButt;
            overviewButt.Source = blueOverviewButt;

            updateGrid(frontPage);

        }

        /************************** 
        * FUNCTION: Updates the grid which contains the reminders for the different pages
        **************************/
        private void updateGrid(Grid grid)
        {
            try
            {
                listController.ResetGrid();
            }
            catch { }

            List<Reminder> reminders = mainController.getTodaysReminders();
            if (grid == overView)
            {
                listController = new ListController(overView, reminders, mainController);
                listController.ListAllOverview();
            }
            else if (grid == frontPage)
            {
                listController = new ListController(frontPage, reminders, mainController);
                listController.ListAllFrontPage();
            }
            else
            {
                listController.setNewReminderList(mainController.getADaysReminders(nameOfDayToNumber(dayOfTheWeekLabel.Content.ToString())));
                listController.ListAllOverview();
            }

        }

        /*--------------------------------Overview--------------------------------*/
        private void overviewButt_MouseEnter(object sender, MouseEventArgs e)
        {
            overviewButt.Source = blueHoverOverviewButt;
        }

        private void overviewButt_MouseLeave(object sender, MouseEventArgs e)
        {
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
            updateGrid(overView);
        }

        private int nameOfDayToNumber(string str)
        {
            switch (str)
            {
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

                //reset days
                Mon.Source = dayButt;
                Tue.Source = dayButt;
                Wed.Source = dayButt;
                Thu.Source = dayButt;
                Fri.Source = dayButt;
                Sat.Source = dayButt;
                Sun.Source = dayButt;
            }
            else
            {
                if (startTime.Text == "" || stopTime.Text == "")
                    popUpLabel.Content = "Choose start and finish time";
                else if (title.Text == "")

                ReminderClear();
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

        /**************************
         * CALL: getSelectedDays() 
         * FUNCTION: gets the days that have been toggled via a  string
         * NOTE: 1 = toggled, 0 = untoggled
         **************************/
        private String getSelectedDays()
        {
            string days = "";

            #region if-else statements for checking toggled days

            if (Mon.Source == dayClickedButt)
                days += "1";
            else
                days += "0";

            if (Tue.Source == dayClickedButt)
                days += "1";
            else
                days += "0";

            if (Wed.Source == dayClickedButt)
                days += "1";
            else
                days += "0";

            if (Thu.Source == dayClickedButt)
                days += "1";
            else
                days += "0";

            if (Fri.Source == dayClickedButt)
                days += "1";
            else
                days += "0";

            if (Sat.Source == dayClickedButt)
                days += "1";
            else
                days += "0";

            if (Sun.Source == dayClickedButt)
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
            updateGrid(null);

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
            updateGrid(null);

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


        #region worlds ugliest code

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string buttContent = (sender as Label).Content.ToString();

            switch (buttContent)
            {
                case "Mon":
                    if (Mon.Source == dayClickedButt)
                        Mon.Source = dayButt;
                    else Mon.Source = dayClickedButt;
                    break;
                case "Tue":
                    if (Tue.Source == dayClickedButt)
                        Tue.Source = dayButt;
                    else Tue.Source = dayClickedButt;
                    break;
                case "Wed":
                    if (Wed.Source == dayClickedButt)
                        Wed.Source = dayButt;
                    else Wed.Source = dayClickedButt;
                    break;
                case "Thu":
                    if (Thu.Source == dayClickedButt)
                        Thu.Source = dayButt;
                    else Thu.Source = dayClickedButt;
                    break;
                case "Fri":
                    if (Fri.Source == dayClickedButt)
                        Fri.Source = dayButt;
                    else Fri.Source = dayClickedButt;
                    break;
                case "Sat":
                    if (Sat.Source == dayClickedButt)
                        Sat.Source = dayButt;
                    else Sat.Source = dayClickedButt;
                    break;
                case "Sun":
                    if (Sun.Source == dayClickedButt)
                        Sun.Source = dayButt;
                    else Sun.Source = dayClickedButt;
                    break;
            }
        }

        private void Label_MouseEnter(object sender, MouseEventArgs e)
        {
            string buttContent = (sender as Label).Content.ToString();

            switch (buttContent)
            {
                case "Mon":
                    if (Mon.Source != dayClickedButt)
                        Mon.Source = dayHoverButt;
                    break;
                case "Tue":
                    if (Tue.Source != dayClickedButt)
                        Tue.Source = dayHoverButt;
                    break;
                case "Wed":
                    if (Wed.Source != dayClickedButt)
                        Wed.Source = dayHoverButt;
                    break;
                case "Thu":
                    if (Thu.Source != dayClickedButt)
                        Thu.Source = dayHoverButt;
                    break;
                case "Fri":
                    if (Fri.Source != dayClickedButt)
                        Fri.Source = dayHoverButt;
                    break;
                case "Sat":
                    if (Sat.Source != dayClickedButt)
                        Sat.Source = dayHoverButt;
                    break;
                case "Sun":
                    if (Sun.Source != dayClickedButt)
                        Sun.Source = dayHoverButt;
                    break;
            }
        }

        private void Label_MouseLeave(object sender, MouseEventArgs e)
        {
            string buttContent = (sender as Label).Content.ToString();

            switch (buttContent)
            {
                case "Mon":
                    if (Mon.Source != dayClickedButt)
                        Mon.Source = dayButt;
                    break;
                case "Tue":
                    if (Tue.Source != dayClickedButt)
                        Tue.Source = dayButt;
                    break;
                case "Wed":
                    if (Wed.Source != dayClickedButt)
                        Wed.Source = dayButt;
                    break;
                case "Thu":
                    if (Thu.Source != dayClickedButt)
                        Thu.Source = dayButt;
                    break;
                case "Fri":
                    if (Fri.Source != dayClickedButt)
                        Fri.Source = dayButt;
                    break;
                case "Sat":
                    if (Sat.Source != dayClickedButt)
                        Sat.Source = dayButt;
                    break;
                case "Sun":
                    if (Sun.Source != dayClickedButt)
                        Sun.Source = dayButt;
                    break;
            }
        #endregion
        }

        public void ReminderClear()
        {
            title.Text = "";
            startTime.Text = "";
            stopTime.Text = "";
        }


    }
}