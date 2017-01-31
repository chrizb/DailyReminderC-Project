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


namespace dailyreminder {
    public partial class MainWindow : Window {
        System.Windows.Forms.NotifyIcon icon = new System.Windows.Forms.NotifyIcon();

		
        // Controllers to be used:
        DispatcherTimer timer = new DispatcherTimer();


        //private void startclock()
        //{
        //    timer.Interval = TimeSpan.FromSeconds(1);
        //    timer.Tick += tickevent;
        //    timer.Start();
        //}

        public MainController mainController;
        ListController listController;

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

            //startclock();

            //testin
           
          
            // Show login-popup
            mainController = new MainController(false);

            mainController.initializeDataAndLogin();

        }


        //private void tickevent(object sender, EventArgs e)
        //{

       
        //   string nowdate =datalbl.Text = DateTime.Now.ToString(@"HH:mm", new CultureInfo("sv-SE"));
        //   // var timezone = TimeZoneInfo.FindSystemTimeZoneById("Atlantic Standard Time");
        //    //string nowdate = datalbl.Text = TimeZoneInfo.ConvertTime(DateTime.Now, timezone).ToString("hh:mm:ss");
        //    string alarm = stopTime.FormatString = "17:10";
        //    title.Text = alarm;
        //    if (alarm == nowdate)
        //    {
        //        this.icon.ShowBalloonTip(0, "Alarmtime", "AlarmTime", System.Windows.Forms.ToolTipIcon.Info);
        //        title.Text = "Alarm";
        //        timer.Stop();
        //    }
        //}
        

        BitmapImage blueButt = new BitmapImage(new Uri("Images/Buttons/blueButt.png", UriKind.Relative));
        BitmapImage blueHoverButt = new BitmapImage(new Uri("Images/Buttons/blueHoverButt.png", UriKind.Relative));
        BitmapImage blueClickedButt = new BitmapImage(new Uri("Images/Buttons/blueClickedButt.png", UriKind.Relative));

        BitmapImage greenButt = new BitmapImage(new Uri("Images/Buttons/addnewButt.png", UriKind.Relative));
        BitmapImage greenHoverButt = new BitmapImage(new Uri("Images/Buttons/addnewHoverButt.png", UriKind.Relative));
        BitmapImage greenClickedButt = new BitmapImage(new Uri("Images/Buttons/addnewClickedButt.png", UriKind.Relative));

        BitmapImage createreminderButt = new BitmapImage(new Uri("Images/Buttons/createreminderButt.png", UriKind.Relative));
        BitmapImage createreminderHoverButt = new BitmapImage(new Uri("Images/Buttons/createreminderHoverButt.png", UriKind.Relative));
        BitmapImage createreminderClickedButt = new BitmapImage(new Uri("Images/Buttons/createreminderClickedButt.png", UriKind.Relative));

        BitmapImage rightArrowButt = new BitmapImage(new Uri("Images/rightArrow.png", UriKind.Relative));
        BitmapImage rightArrowHoverButt = new BitmapImage(new Uri("Images/rightArrowHover.png", UriKind.Relative));
        BitmapImage rightArrowClickedButt = new BitmapImage(new Uri("Images/rightArrowPressed.png", UriKind.Relative));

        String[] daysOfWeek = new String[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

        #region Mouseevents for Menu Buttons

        /*--------------------------------Add Reminder--------------------------------*/
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
        /*--------------------------------Frontpage--------------------------------*/
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


            // List the reminders
            try {
                listController.ResetGrid();
            } catch { }
            
            List<Reminder> reminders = mainController.getTodaysReminders();
            listController = new ListController(frontPage, reminders);
            listController.ListAll();
            

            

        }
        /*--------------------------------Overview--------------------------------*/
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

            dayOfTheWeekLabel.Content = DateTime.Now.DayOfWeek;
        }


            dayOfTheWeekLabel.Content = DateTime.Now.DayOfWeek;
        }

        #endregion


        /************************** 
         * FUNCTION: Creates a reminder and adds it to a list when createButt is pressed
         **************************/
        private void createButt_MouseDown(object sender, MouseButtonEventArgs e)
        {
            createButt.Source = createreminderClickedButt;
            
            // Gives the user a notification on the system tray 
            this.icon.ShowBalloonTip(10000, "Added Reminder", "AlarmTime", System.Windows.Forms.ToolTipIcon.Info);
            
            Reminder newReminder = new Reminder();
            newReminder.Title = title.Text;
            newReminder.Description = description.Text;
            newReminder.startTime = (startTime.Value.Value.Hour * 60) + startTime.Value.Value.Minute;
            newReminder.endTime = (stopTime.Value.Value.Hour * 60) + stopTime.Value.Value.Minute;
            newReminder.Days = getSelectedDays();
            
            mainController.addReminderToList(newReminder);
            
        }

        private void createButt_MouseEnter(object sender, MouseEventArgs e)
        {
            createButt.Source = createreminderHoverButt;
        }

        private void createButt_MouseLeave(object sender, MouseEventArgs e)
        {
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

        #region arrowButtons

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
        }
        private void rightArrow_MouseEnter(object sender, MouseEventArgs e)
        {
            rightArrow.Source = rightArrowHoverButt;
        }

        private void rightArrow_MouseLeave(object sender, MouseEventArgs e)
        {
            rightArrow.Source = rightArrowButt;
        }

        #endregion

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
        }

     

    }
}