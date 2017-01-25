using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dailyreminder.models;

namespace dailyreminder.controllers {
    public class MainController {

        private List<Reminder> reminderList;
        public bool loggedIn{ get; set; }
        ReminderDataController rdc;


        public MainController(bool loggedIn) {
            this.loggedIn = loggedIn;
            if(loggedIn){ // This is important, both is used the same way but works different
                rdc = new LoggedInController();
            }
            else {
                rdc = new OfflineController();
                reminderList = new List<Reminder>();
            }
            
        }

        public void addReminderToList(Reminder newReminder) {
            reminderList.Add(newReminder);
            saveCurrentReminderList();
        }
        public void deleteReminderFromList(int index) {
            reminderList.RemoveAt(index);
            saveCurrentReminderList();
        }

        public void initializeDataAndLogin() {
            reminderList = rdc.loadAll(@"c:\tempFile.dr");
        }
        public void saveCurrentReminderList() {
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
