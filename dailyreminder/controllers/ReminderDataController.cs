using dailyreminder.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;


namespace dailyreminder.controllers {
    abstract class ReminderDataController {
        abstract public void saveAll(List<Reminder> reminderList, string token);
        abstract public List<Reminder> loadAll(string filepath);
        abstract public void saveReminder(Reminder reminder);
        abstract public bool login(string email, string token);
        abstract public void addNewReminder(Reminder reminder);
        abstract public void deleteReminder(int reminderId);
        abstract public void updateReminder(Reminder reminder);
    }
}
