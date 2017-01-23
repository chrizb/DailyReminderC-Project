using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dailyreminder.models;

namespace dailyreminder.controllers {
    class LoggedInController : ReminderDataController{
        
        public override void saveAll(List<Reminder> reminderList, string token) { }

        public override List<Reminder> loadAll(string filepath) { return null; }
        public override void saveReminder(Reminder reminder) { }
        public override bool login(string email, string token) { return false; }
        public override void addNewReminder(Reminder reminder) { }
        public override void deleteReminder(int reminderId) { }
        public override void updateReminder(Reminder reminder) { }

    }
}
