using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dailyreminder.models;
using System.Windows.Controls;

namespace dailyreminder.controllers {
    public class MainController {

        private List<Reminder> reminderList;
        public bool loggedIn{ get; set; }
        ReminderDataController rdc;
        MainWindow mainWindow;
        long highestId;


        public MainController(bool loggedIn, MainWindow mw) {
            this.loggedIn = loggedIn;
            if(loggedIn){ // This is important, both is used the same way but works different
                rdc = new LoggedInController();
            }
            else {
                rdc = new OfflineController();
                reminderList = new List<Reminder>();
            }

            highestId = 0;
            foreach (Reminder reminder in reminderList) { // set the highest ID on reminders
                if (reminder.Id > highestId)
                    highestId = reminder.Id;
            }
            mainWindow = mw;
        }

        public void addReminderToList(Reminder newReminder) {
            highestId += 1;
            newReminder.Id = highestId;
            reminderList.Add(newReminder);
            saveCurrentReminderList();
        }
        public void deleteReminderFromList(long id) {
            for (int i = 0; i < reminderList.Count; i++) {
                if (reminderList.ElementAt(i).Id == id) {
                    reminderList.RemoveAt(i);
                }
            }
                saveCurrentReminderList();
        }

        public Reminder getReminderById(long id)
        {
            foreach(Reminder reminder in reminderList)
            {
                if (reminder.Id == id)
                    return reminder;
            }
            return null;
        }

        public void editFunction(long id)
        {
            mainWindow.setupEditPage(id);
        }
        public void initializeDataAndLogin() {
            reminderList = rdc.loadAll(@"c:\data\tempFile.dr");
            
        }
        public void saveCurrentReminderList() {
            rdc.saveAll(reminderList, null);
        }

        public List<Reminder> getTodaysReminders() {
            int today = ((int)DateTime.Now.DayOfWeek - 1 + 7) % 7;
            List<Reminder> todaysReminders = new List<Reminder>();
            foreach (Reminder reminder in reminderList) {
                if (reminder.Days.ElementAt(today) == '1') { // Checks if the event is happening today
                    todaysReminders.Add(reminder);
                }
            }
            return todaysReminders.OrderBy(o => o.endTime).ToList();
        }

        public List<Reminder> getADaysReminders(int dt) {
            List<Reminder> todaysReminders = new List<Reminder>();
            foreach (Reminder reminder in reminderList) {
                if (reminder.Days.ElementAt(dt) == '1') { // Checks if the event is happening today
                    todaysReminders.Add(reminder);
                }
            }
            return todaysReminders.OrderBy(o => o.endTime).ToList();
        }

        public void setReminderToDone(long id) {
            foreach (Reminder reminder in reminderList) {
                if (reminder.Id == id) {
                    reminder.setToDone();
                    reminder.dateSetToDone = DateTime.Now;
                }
            }
            saveCurrentReminderList();
        }
    }
}
