using dailyreminder.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;


namespace dailyreminder.controllers {
    abstract class ReminderDataController {
        public void saveAll(List<Reminder> reminderList, string token);
        public List<Reminder> loadAll(string filepath);
        public void saveReminder(Reminder reminder);
        public bool login(string email, string token);
        public void addNewReminder(Reminder reminder);
        public void deleteReminder(int reminderId);
        public void updateReminder(Reminder reminder);
    }
}
