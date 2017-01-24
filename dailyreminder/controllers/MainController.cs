﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dailyreminder.models;

namespace dailyreminder.controllers {
    class MainController {

        public List<Reminder> reminderList { get; set; }
        public bool loggedIn{ get; set; }
        ReminderDataController rdc;


        public MainController(bool loggedIn) {
            this.loggedIn = loggedIn;
            if(loggedIn){ // This is important, both is used the same way but works different
                rdc = new LoggedInController();
            }
            else {
                rdc = new OfflineController();
            }
            
        }

        public void initializeDataAndLogin() {
            reminderList = rdc.loadAll(@"c:\tempFile.dr");
        }
        public void saveDataAndExitProgram() {
            rdc.saveAll(reminderList, null);
        }

        public List<Reminder> getTodaysReminders() {
            DateTime dt = new DateTime();
            int today = (int)dt.DayOfWeek;
            List<Reminder> todaysReminders = new List<Reminder>();
            foreach (Reminder reminder in reminderList) {
                if (reminder.Days.ElementAt(today) == 1) { // Checks if the event is happening today
                    todaysReminders.Add(reminder);
                }
            }
            return todaysReminders.OrderBy(o => o.endTime).ToList();
        }
    }
}
